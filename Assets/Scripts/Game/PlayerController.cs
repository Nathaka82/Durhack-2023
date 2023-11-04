using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float MaxHealth = 100f;
    private float Health;

    private bool _isGrounded;
    private const float JumpForce = 15f;

    private float RayLength = 0.55f;
    private Rigidbody _rb;

    private float gravity = -50f;

    public float Speed = 3f;

    public GameObject WeaponPrefab;
    private GameObject WeaponObj;
    private Weapon Weapon;

    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        RayLength = 0.55f * transform.localScale.y;

        _rb = GetComponent<Rigidbody>();
        _isGrounded = false;

        Health = MaxHealth;

        WeaponObj = Instantiate(WeaponPrefab, transform.Find("Hand"));
        Weapon = WeaponObj.GetComponent<Weapon>();

        anim = GetComponentInChildren<Animator>();
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
            _isGrounded = Physics.Raycast(ray, RayLength, 3);
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
        if (Health <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        Health -= amount;
    }

    private void Die()
    {
        Debug.Log("You Died!");
        Destroy(gameObject);
    }

    private void Attack()
    {
        Weapon.Attack();
    }
}