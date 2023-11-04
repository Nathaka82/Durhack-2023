using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHealth = 100f;
    private float Health;

    private bool _isGrounded;
    private const float JumpForce = 15f;

    private float RayLength = 0.25f;
    private Rigidbody _rb;

    private float gravity = -50f;

    public float Speed = 3f;

    // public GameObject WeaponPrefab;
    private GameObject WeaponObj;
    private Weapon Weapon;

    private Animator anim;

    private Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        RayLength = 0.25f * transform.localScale.y;

        _rb = GetComponent<Rigidbody>();
        _isGrounded = false;

        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Magnitude(transform.position - Player.position);
        if (dist <= 5)
        {
            Vector3 dir = Vector3.Normalize(Player.position - transform.position);
            transform.position += dir * Speed * Time.deltaTime;
        }

        if (_isGrounded)
        {
            float num = Random.Range(0, 1000);
            if (num >= 995)
            {
                Jump();
            }
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
        Debug.DrawRay(transform.position, Vector3.down * RayLength);
    }

    void Jump()
    {
        _isGrounded = false;
        _rb.velocity = Vector3.up * JumpForce;
    }
}
