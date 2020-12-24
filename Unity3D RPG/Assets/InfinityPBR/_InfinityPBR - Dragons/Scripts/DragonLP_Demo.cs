using UnityEngine;

public class DragonLP_Demo : MonoBehaviour
{
    public Renderer[] renderer;
    public GameObject[] heads;
    public GameObject[] tails;
    public GameObject[] backs;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Randomize();
        }
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
        for (int i = 0; i < heads.Length; i++)
        {
            heads[i].SetActive(false);
        }
        for (int i = 0; i < tails.Length; i++)
        {
            tails[i].SetActive(false);
        }
        for (int i = 0; i < backs.Length; i++)
        {
            backs[i].SetActive(false);
        }
        heads[Random.Range(0, heads.Length)].SetActive(true);
        tails[Random.Range(0, tails.Length)].SetActive(true);
        backs[Random.Range(0, backs.Length)].SetActive(true);
    }

    public bool GetRandom()
    {
        if (Random.Range(0, 2) == 0)
            return false;

        return true;
    }
}
