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

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Anim.SetFloat("AttackSpeed", 1 / AttackSpeed);
        Anim.Play("Sword_Swing");
    }
}
