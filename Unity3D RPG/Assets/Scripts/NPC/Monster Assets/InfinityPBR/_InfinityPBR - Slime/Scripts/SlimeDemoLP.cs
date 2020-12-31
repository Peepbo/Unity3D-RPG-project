using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDemoLP : MonoBehaviour
{

    public Renderer[] renderer;
    public Renderer[] rendererBody;
    public Animator animator;
    public GameObject[] bodyParts;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Randomize();
        }
    }
    
    public void Locomotion(float value){
        animator.SetFloat("locomotion", value);
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
    
    public void SetOpacityBody(float value)
    {
        for (int i = 0; i < rendererBody.Length; i++)
        {
            rendererBody[i].material.SetFloat("_Opacity", value);
        }
    }
    
    public void SetHueBody(float value)
    {
        for (int i = 0; i < rendererBody.Length; i++)
        {
            rendererBody[i].material.SetFloat("_Hue", value);
        }
    }
    
    public void SetSaturationBody(float value)
    {
        for (int i = 0; i < rendererBody.Length; i++)
        {
            rendererBody[i].material.SetFloat("_Saturation", value);
        }
    }
    
    public void SetValueBody(float value)
    {
        for (int i = 0; i < rendererBody.Length; i++)
        {
            rendererBody[i].material.SetFloat("_Value", value);
        }
    }
    
    public void Randomize()
    {
        GetComponent<SFB_BlendShapesManager>().RandomizeAll();
        SetHue(Random.Range(0f,1f));
        SetHueBody(Random.Range(0f,1f));
        SetSaturationBody(Random.Range(-0.5f, 0.5f));
        SetValueBody(Random.Range(-0.2f, 0.4f));
        SetOpacityBody(Random.Range(0.3f, 1f));
        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (Random.Range(0,2) == 1)
                bodyParts[i].SetActive(true);
            else
                bodyParts[i].SetActive(false);
        }
        
    }

}
