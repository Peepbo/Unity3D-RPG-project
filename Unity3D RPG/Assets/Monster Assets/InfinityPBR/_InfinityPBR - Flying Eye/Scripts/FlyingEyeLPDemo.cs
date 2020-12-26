using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeLPDemo : MonoBehaviour
{
    public Animator animator;
    public Renderer renderer;
    public Renderer renderer2;
    public GameObject[] randomObjects;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator> ();
    }
	
    public void Locomotion(float newValue){
        animator.SetFloat ("locomotion", newValue);
    }

    public void AlertLevel(float newValue){
        animator.SetFloat ("alertLevel", newValue);
    }

    public void SetHue(float value)
    {
        renderer.material.SetFloat("_Hue", value);
    }
    
    public void SetSaturation(float value)
    {
        renderer.material.SetFloat("_Saturation", value);
    }
    
    public void SetValue(float value)
    {
        renderer.material.SetFloat("_Value", value);
    }
    
    public void SetHue2(float value)
    {
        renderer2.material.SetFloat("_Hue", value);
    }
    
    public void SetSaturation2(float value)
    {
        renderer2.material.SetFloat("_Saturation", value);
    }
    
    public void SetValue2(float value)
    {
        renderer2.material.SetFloat("_Value", value);
    }

    public void Randomize(bool objects)
    {
        SetHue(Random.Range(0f,1f));
        SetHue2(Random.Range(0f,1f));
        if (objects)
        {
            for (int i = 0; i < randomObjects.Length; i++)
            {
                randomObjects[i].SetActive(GetRandom());
            }
        }
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Randomize(false);
            GetComponent<SFB_BlendShapesManager>().RandomizeAll();
        }
    }
    
    public bool GetRandom()
    {
        if (Random.Range(0, 2) == 0)
            return false;

        return true;
    }
    
}
