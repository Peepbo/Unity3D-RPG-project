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
    private MainPlayer playerInput;

    private void Awake()
    {
        playerInput = new MainPlayer();
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
        Vector2 _delta = playerInput.playerMain.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += _delta.x * 200* lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += -_delta.y * lookSpeed * Time.deltaTime;
    }

}
