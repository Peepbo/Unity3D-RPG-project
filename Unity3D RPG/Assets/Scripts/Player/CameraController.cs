using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    
    public TouchBoard touchBoard;
    protected float cameraAngleX;
    protected float cameraAngleY;
    protected float cameraAngleSpeed =0.2f;

    void Start()
    {
        Input.multiTouchEnabled = true;
        //touchBoard = new TouchBoard(); 
    }

    // Update is called once per frame
    void Update()
    {
        cameraAngleX += touchBoard.TouchDist.x * cameraAngleSpeed;
       
        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(cameraAngleX,Vector3.up)* new Vector3(0, 3, -4);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);

    }

   

}

