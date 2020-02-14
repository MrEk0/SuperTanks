using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [SerializeField] float healthPoint = 3f;
    [SerializeField] float tintFadeSpeed = 5f;
    [SerializeField] UnityEvent receiveDamage;
    [SerializeField] UnityEvent onDead;

    Material material;
    Color attackColor;
    Color healColor;

    const string AttackColorName = "_TintAttack";
    const string HealColorName = "_TintHeal";

    public void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        attackColor = material.GetColor(AttackColorName);
        healColor = material.GetColor(HealColorName);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            TakeDamage();
        }
    }

    public override void TakeDamage()
    {
        attackColor.a = 1f;
        material.SetColor(AttackColorName, attackColor);
        StartCoroutine(FadeTint());

        healthPoint = Mathf.Max(0, healthPoint - 1);

        if (healthPoint == 0f)
        {
            onDead.Invoke();
        }

        receiveDamage.Invoke();
    }

    IEnumerator FadeTint()
    {
        while (attackColor.a > 0)
        {
            attackColor.a = Mathf.Clamp01(attackColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor(AttackColorName, attackColor);
            yield return null;
        }
    }

    public override IEnumerator TakeHeal()
    {
        while (healColor.a > 0)
        {
            healColor.a = Mathf.Clamp01(healColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor(AttackColorName, healColor);
            //return base.TakeHeal();
            yield return null;
        }
      
    }
}
