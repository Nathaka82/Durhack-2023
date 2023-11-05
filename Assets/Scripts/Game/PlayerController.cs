using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerController : MonoBehaviour
{
    public TMP_Text GameOver;

    public Slider PlayerHealthBar;

    private float MaxHealth = 100f;
    private float Health;

    private bool _isGrounded;
    private const float JumpForce = 15f;

    private float RayLength = 0.55f;
    private Rigidbody _rb;

    private float gravity = -50f;

    public float Speed = 3f;

    private int Score;

    public GameObject WeaponPrefab;
    private GameObject WeaponObj;
    private Weapon Weapon;

    private Animator anim;

    public LayerMask EnemyLayer;

    private AudioSource AS;

    private float TimeSinceLastAttack;
    private bool dead = false;

    // Start is called before the first frame update
    private void Start()
    {
        AS = GetComponent<AudioSource>();

        RayLength = 0.55f * transform.localScale.y;

        _rb = GetComponent<Rigidbody>();
        _isGrounded = false;

        Health = MaxHealth;
        PlayerHealthBar.value = Health / MaxHealth;

        WeaponObj = Instantiate(WeaponPrefab, transform.Find("Hand"));
        Weapon = WeaponObj.GetComponent<Weapon>();

        anim = GetComponentInChildren<Animator>();

        TimeSinceLastAttack = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        #region Inputs

        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _rb.position += Vector3.forward * Speed * Time.deltaTime;
            velocity += Vector3.forward * Speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rb.position += Vector3.back * Speed * Time.deltaTime;
            velocity += Vector3.back * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.position += Vector3.left * Speed * Time.deltaTime;
            velocity += Vector3.left * Speed;
            transform.localScale = new Vector3(-MathF.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.position += Vector3.right * Speed * Time.deltaTime;
            velocity += Vector3.right * Speed;
            transform.localScale = new Vector3(MathF.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        anim.SetFloat("Speed", velocity.magnitude);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
        else
        {
            var ray = new Ray
            {
                origin = transform.position,
                direction = Vector3.down
            };
            _isGrounded = Physics.Raycast(ray, RayLength);
            _rb.velocity += Vector3.up * gravity * Time.deltaTime;
        }

        #endregion
    }

    void Jump()
    {
        _isGrounded = false;
        _rb.velocity = Vector3.up * JumpForce;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void Damage(float amount)
    {
        Health -= amount;
        PlayerHealthBar.value = Health / MaxHealth;
        if (Health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }
    public void Heal(float amount)
    {
        Health += amount;
    }

    private void Die()
    {
        Debug.Log("You Died!");
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        AS.Play();
        StartCoroutine(_Die());
    }

    IEnumerator _Die()
    {
        float time = 1.175f;
        anim.Play("FinnDie");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        GameOver.gameObject.SetActive(true);
    }

    private void Attack()
    {
        float time = Time.time;
        if (time - TimeSinceLastAttack >= Weapon.AttackSpeed)
        {
            TimeSinceLastAttack = time;
            Weapon.Attack();
            var hits = Physics.BoxCastAll(transform.position + Vector3.right * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), new Vector3(0.5f, 1f, 0.5f), Vector3.right * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), Quaternion.identity, 1, EnemyLayer);
            foreach (var enemyObject in hits)
            {
                var enemy = enemyObject.transform.GetComponent<Enemy>();
                enemy.Damage(Weapon.Damage);
            }
        }
    }
}