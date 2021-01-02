using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Manager : MonoBehaviour {

    public GameObject[] GOs;
    public int currIndx;
    public bool twins, animate, freez, rotate, capture;
	public int rotationStep, rotationCounter;
    Transform thisTransform;
    public Animator anim;

    public void Start()
    {
        thisTransform = transform;
        anim = GetComponent<Animator>();
    }

    public void LateUpdate()
    {
        if (capture)
        {
            ScreenCapture.CaptureScreenshot(@"H:\AssetStore\Image-"+rotationCounter.ToString("00")+".png", 1);
            //capture = false;
			if(rotate)
			{
				rotationCounter++;
				thisTransform.eulerAngles = new Vector3(0, rotationStep * -rotationCounter, 0);
				
			}
        }

        if (animate)
        {
            if (freez && thisTransform.eulerAngles.y != -50)
                thisTransform.eulerAngles = new Vector3(0, -50, 0);
        }
		
		
    }
}
