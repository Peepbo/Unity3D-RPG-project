using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEft : MonoBehaviour
{
    [SerializeField] private Renderer[] healthRenderers = new Renderer[1];

    private float targetDissolveValue = 1f;
    private float currentDissolveValue = 1f;

    public float dissolveSpeed = 2f;

    public void SetValue(float value)
    {
        targetDissolveValue = value;
    }

    // Update is called once per frame
    void Update()
    {
        currentDissolveValue = Mathf.Lerp(currentDissolveValue, targetDissolveValue, dissolveSpeed * Time.deltaTime);

        for(int i = 0; i < healthRenderers.Length; i++)
            healthRenderers[i].material.SetFloat("_Health", currentDissolveValue);
    }
}
