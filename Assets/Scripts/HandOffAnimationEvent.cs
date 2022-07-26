using System;
using UnityEngine;

public class HandOffAnimationEvent : MonoBehaviour
{
    public event Action OnWeaponRelease;

    // This is the animation event, defined/called by animation
    public void WeaponRelease()
    {
        OnWeaponRelease?.Invoke();
    }
}
