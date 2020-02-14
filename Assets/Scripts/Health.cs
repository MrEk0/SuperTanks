using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    //private void GrabHeal()
    //{
    //    if (healColor.a > 0)
    //    {
    //        healColor.a = Mathf.Clamp01(healColor.a - tintFadeSpeed * Time.deltaTime);
    //        material.SetColor(AttackColorName, healColor);
    //    }
    //}
    public abstract void TakeDamage();

    public virtual IEnumerator TakeHeal()
    {
        yield return null;
    }
}
