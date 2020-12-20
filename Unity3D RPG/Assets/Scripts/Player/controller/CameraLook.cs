using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 1;
    private CinemachineFreeLook cinemachine;
    private PlayerMain playerInput;

    private void Awake()
    {
        playerInput = new PlayerMain();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = playerInput.Playermain.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 200* lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += -delta.y * lookSpeed * Time.deltaTime;
    }

}
