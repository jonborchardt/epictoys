using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

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

    private void ShootAction_OnShoot(object sender, EventArgs e)
    {
        animator.SetTrigger("Shoot");
    }
}
