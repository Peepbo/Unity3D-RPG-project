using UnityEngine;

public class MedusaLP_Demo : MonoBehaviour
{

    public Renderer[] renderer;
    public SFB_BlendShapesManager[] bsmanager;
    public GameObject canvas;
    
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
    
    }
    
    
    
    
    public GameObject[] models;
    public SFB_StayWithTarget[] trackingObjects;
    public SFB_CameraRotate_v2[] rotateObjects;
    private int modelIndex = 0;

    
    public void SwitchModel(int dir = 1)
    {
        modelIndex += dir;
        if (modelIndex < 0)
        {
            modelIndex = models.Length - 1;
        }

        if (modelIndex > models.Length - 1)
        {
            modelIndex = 0;
        }

        for (int i = 0; i < trackingObjects.Length; i++)
        {
            trackingObjects[i].target = models[modelIndex];
        }
        
        for (int i = 0; i < rotateObjects.Length; i++)
        {
            rotateObjects[i].target = models[modelIndex].transform;
        }
    }
}
