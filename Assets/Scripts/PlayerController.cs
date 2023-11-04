using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float MaxHealth = 100f;
    private float Health;

    private bool _isGrounded;
    private const float JumpForce = 5f;

    private const float RayLength = 0.55f;
    private Rigidbody _rb;

    public float Speed = 3f;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _isGrounded = false;

        Health = MaxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        #region Inputs

        if (Input.GetKey(KeyCode.W)) _rb.position += Vector3.forward * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) _rb.position += Vector3.back * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) _rb.position += Vector3.left * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) _rb.position += Vector3.right * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _rb.velocity = Vector3.up * JumpForce;
            _isGrounded = false;
        }
        else
        {
            var ray = new Ray
            {
                origin = transform.position,
                direction = Vector3.down
            };
            _isGrounded = Physics.Raycast(ray, RayLength);
            // Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.red);
        }

        #endregion

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

    }
}