using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponObject", order = 1)]
public class Weapon : ScriptableObject
{
    public enum WeaponTypes
    {
        Melee,
        Ranged,
        Magic
    }

    public Sprite WeaponSprite;

    public WeaponTypes WeaponType;

    public float Damage;
    public float AttackSpeed;

    private Animation _animator;
    public AnimationClip AttackAnimation;

    public void Attack()
    {
        _animator.Play(AttackAnimation.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
