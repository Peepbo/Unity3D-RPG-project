using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManDemoLP : MonoBehaviour
{
    private Animator animator;
    public Renderer[] renderer;
    public GameObject canvas;
    public GameObject[] parts;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Locomotion(float locomotion)
    {
        animator.SetFloat("Locomotion", locomotion);
    }

    public void SetHue(float value)
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetFloat("_Hue", value);
        }
    }
    
    public void SetSaturation(float value)
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetFloat("_Saturation", value);
        }
    }
    
    public void SetValue(float value)
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetFloat("_Value", value);
        }
    }

    public void Randomize()
    {
        GetComponent<SFB_BlendShapesManager>().RandomizeAll();
        SetHue(Random.Range(0f,1f));
        SetSaturation(Random.Range(-.25f,.25f));
        SetValue(Random.Range(-.1f,.1f));

        for (int i = 0; i < parts.Length; i++)
        {
            if (Random.Range(0, 2) == 1)
                parts[i].SetActive(true);
            else
            {
                parts[i].SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Randomize();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }
    
}
