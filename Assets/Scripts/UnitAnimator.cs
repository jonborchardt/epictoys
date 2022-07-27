using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    public event EventHandler OnWeaponRelease;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform bulletProjectilePrefab;

    [SerializeField]
    private Transform shootPointTransform;

    private ShootAction.OnShootEventArgs lastShootArgs;

    private HandOffAnimationEvent handOffAnimationEvent;

    void OnEnable()
    {
        handOffAnimationEvent = GetComponentInChildren<HandOffAnimationEvent>();
        handOffAnimationEvent.OnWeaponRelease += WeaponRelease;
    }

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        animator.SetFloat("IsWalking", 1);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        animator.SetFloat("IsWalking", 0);
    }

    private void ShootAction_OnShoot(
        object sender,
        ShootAction.OnShootEventArgs e
    )
    {
        // when this animation relesaes, we call WeaponRelease
        animator.SetTrigger("Shoot");
        lastShootArgs = e;
    }

    // called via animation
    private void WeaponRelease()
    {
        Transform bulletProjectileTransform =
            Instantiate(bulletProjectilePrefab,
            shootPointTransform.position,
            Quaternion.identity);
        BulletProjectile bulletProjectile =
            bulletProjectileTransform.GetComponent<BulletProjectile>();
        Vector3 targetShootAtPos = lastShootArgs.TargetUnit.GetWorldPosition();

        // note: will only shoot horizontally
        targetShootAtPos.y = shootPointTransform.position.y;
        bulletProjectile.SetUp (targetShootAtPos);
        OnWeaponRelease?.Invoke(this, EventArgs.Empty);
    }
}
