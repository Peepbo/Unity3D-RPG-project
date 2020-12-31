using UnityEngine;

public class RockMonsterLP_Demo : MonoBehaviour
{
    public Renderer[] renderer;
    public SFB_BlendShapesManager[] bsmanager;
    public GameObject canvas;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Randomize();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCanvas();
        }
    }
    
    public void Locomotion(float newValue){
        animator.SetFloat ("locomotion", newValue);
    }

    public void ToggleCanvas()
    {
        canvas.SetActive(!canvas.active);
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
        for (int i = 0; i < bsmanager.Length; i++)
        {
            bsmanager[i].RandomizeAll();
        }
        SetHue(Random.Range(0f,1f));
        SetSaturation(Random.Range(-.2f,.2f));
        SetValue(Random.Range(-.2f,.2f));
    
    }
}
