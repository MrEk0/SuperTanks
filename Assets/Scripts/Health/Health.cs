using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SuperTanks.Tanks
{
    public abstract class Health : MonoBehaviour
    {
        public abstract void TakeDamage();

        public virtual IEnumerator TakeHeal()
        {
            yield return null;
        }
    }
}
