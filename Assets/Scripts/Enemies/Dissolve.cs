using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] float timeToSee=2f;
    [SerializeField] float timeToBeInvisible=4f;
    
    Material material;
    float fadeTime = 1f;
    float timeSinceBecameVisible = 0f;
    float timeSinceBecameInvisible = 0f;
    bool isVisible = true;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        timeSinceBecameVisible += Time.deltaTime;
        timeSinceBecameInvisible += Time.deltaTime;

        FadeIn();

        FadeOut();

    }

    private void FadeOut()
    {
        if (timeSinceBecameInvisible > timeToBeInvisible && !isVisible)
        {
            fadeTime += Time.deltaTime;

            if (fadeTime >= 1)
            {
                fadeTime = 1;
                isVisible = true;
                timeSinceBecameVisible = 0f;
            }
            material.SetFloat("_Fade", fadeTime);
        }
    }

    private void FadeIn()
    {
        if (timeSinceBecameVisible > timeToSee && isVisible)
        {
            fadeTime -= Time.deltaTime;

            if (fadeTime <= 0)
            {
                fadeTime = 0;
                isVisible = false;
                timeSinceBecameInvisible = 0f;
            }
            material.SetFloat("_Fade", fadeTime);
        }
    }
}
