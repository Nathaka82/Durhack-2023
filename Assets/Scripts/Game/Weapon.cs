using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponTypes
    {
        Melee,
        Ranged,
        Magic
    }

    public WeaponTypes WeaponType;

    public float Damage;
    public float AttackSpeed;

    private Animator Anim;

    private float TimeSinceLastAttack;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        TimeSinceLastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Anim.SetFloat("AttackSpeed", 1 / AttackSpeed);
        float time = Time.time;
        if (time - TimeSinceLastAttack >= AttackSpeed)
        {
            TimeSinceLastAttack = time;
            Anim.Play("Sword_Swing");
        }
    }
}
