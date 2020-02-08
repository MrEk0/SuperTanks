using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoint = 3f;
    [SerializeField] float tintFadeSpeed = 5f;

    Material material;
    Color attackColor;
    Color healColor;

    const string AttackColorName = "_TintAttack";
    const string HealColorName = "_TintHeal";

    public event Action onGetAttacked;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        attackColor=material.GetColor(AttackColorName);
        healColor = material.GetColor(HealColorName);
    }

    private void Update()
    {
        if (attackColor.a > 0)
        {
            attackColor.a = Mathf.Clamp01(attackColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor(AttackColorName, attackColor);
        }

        if (healColor.a > 0)
        {
            healColor.a = Mathf.Clamp01(healColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor(AttackColorName, healColor);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            attackColor.a = 1f;
            material.SetColor(AttackColorName, attackColor);
            healthPoint = Mathf.Max(0, healthPoint - 1);
            onGetAttacked();

            if (healthPoint == 0f)
            {
                Debug.Log("Gameover");
            }
        }
    }
}
