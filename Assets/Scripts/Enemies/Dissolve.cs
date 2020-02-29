using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] float timeToSee=2f;
    [SerializeField] float timeToBeInvisible=4f;
    [SerializeField] float fadeTime = 2f;
    
    Material material;
    float currentfadeTime;
    float timeSinceBecameVisible = 0f;
    float timeSinceBecameInvisible = 0f;
    bool isVisible = true;

    private void Awake()
    {
        currentfadeTime = fadeTime;
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
            currentfadeTime += Time.deltaTime;

            if (currentfadeTime >= fadeTime)
            {
                currentfadeTime = fadeTime;
                isVisible = true;
                timeSinceBecameVisible = 0f;
            }
            material.SetFloat("_Fade", currentfadeTime/fadeTime);
        }
    }

    private void FadeIn()
    {
        if (timeSinceBecameVisible > timeToSee && isVisible)
        {
            currentfadeTime -= Time.deltaTime;

            if (currentfadeTime <= 0)
            {
                currentfadeTime = 0;
                isVisible = false;
                timeSinceBecameInvisible = 0f;
            }
            material.SetFloat("_Fade", currentfadeTime/fadeTime);
        }
    }
}
