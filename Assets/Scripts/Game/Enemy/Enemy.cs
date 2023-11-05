using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Slider EnemyHealthBar;

    public float MaxHealth = 100f;
    private float Health;

    private bool _isGrounded;
    private const float JumpForce = 15f;

    private float RayLength = 0.25f;
    private Rigidbody _rb;

    private float gravity = -50f;

    public float Speed = 3f;

    public float Attack = 5f;
    public float AttackSpeed = 1f;
    private float TimeSinceLastAttack = 0f;

    public float DetectionRadius = 5f;

    public int CreditCost;

    private AudioSource AS;

    // public GameObject WeaponPrefab;
    private GameObject WeaponObj;
    private Weapon Weapon;

    private Animator anim;

    private Transform Player;

    private SpriteRenderer sr;

    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        RayLength = 0.25f * transform.localScale.y;

        _rb = GetComponent<Rigidbody>();
        _isGrounded = false;

        Health = MaxHealth;
        EnemyHealthBar.value = Health / MaxHealth;

        sr = transform.GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();

        var psMain = ps.main;
        psMain.startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, 1f * Time.deltaTime);
        if (Health > 0)
        {
            var dist = Vector3.Magnitude(transform.position - Player.position);
            if (dist <= DetectionRadius)
            {
                Vector3 dir = Vector3.Normalize(Player.position - transform.position);
                dir.y = 0;
                transform.position += dir * Speed * Time.deltaTime;

                sr.flipX = dir.x < 0;
                if (_isGrounded)
                {
                    float num = Random.Range(0, 1000);
                    if (num >= 995)
                    {
                        Jump();
                    }
                }
            }

            if (!_isGrounded)
            {
                _rb.velocity += Vector3.up * gravity * Time.deltaTime;
                if (_rb.velocity.y <= 0)
                {
                    var ray = new Ray
                    {
                        origin = transform.position,
                        direction = Vector3.down
                    };
                    _isGrounded = Physics.Raycast(ray, RayLength);
                }
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var time = Time.time;
            if (time - TimeSinceLastAttack >= AttackSpeed)
            {
                TimeSinceLastAttack = time;
                var PlayerController = collision.gameObject.GetComponent<PlayerController>();
                PlayerController.Damage(Attack);
            }
        }
    }

    void Jump()
    {
        _isGrounded = false;
        _rb.velocity = Vector3.up * JumpForce;
    }

    public void Damage(float amount)
    {
        AS.Play();

        if (Health <= 0) return;

        ps.Play();
        Health -= amount;
        EnemyHealthBar.value = Health / MaxHealth;
        if (Health <= 0)
        {
            _rb.velocity = Vector3.zero;
            transform.GetComponent<CapsuleCollider>().enabled = false;
            anim.SetTrigger("Die");
            return;
        }
        _rb.AddForce(Vector3.right * 10f * (sr.flipX ? 1 : -1), ForceMode.Impulse);
    }
    public void Heal(float amount)
    {
        Health += amount;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
