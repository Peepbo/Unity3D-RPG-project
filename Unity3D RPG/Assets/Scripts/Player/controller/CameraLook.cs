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
    public TouchBoard touchBoard;
    public CinemachineFreeLook cinemachine2;

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

    private void FixedUpdate()
    {
        if(PlayerData.Instance.isCameraBack)
        {
            cinemachine.m_XAxis.Value += touchBoard.TouchDist.x * 15f * lookSpeed * Time.deltaTime;
            cinemachine.m_YAxis.Value += -touchBoard.TouchDist.y * lookSpeed * Time.deltaTime / 20;
        }
        else
        {
            //cinemachine2.m_XAxis.Value += touchBoard.TouchDist.x * 15f * lookSpeed * Time.deltaTime;

        }

    }
    // Update is called once per frame
    void Update()
    {
        //Vector2 _delta = playerInput.PlayerMain.Look.ReadValue<Vector2>();
        
    }
    public void CameraChange()
    {
        if(PlayerData.Instance.isCameraBack)
        {
            PlayerData.Instance.isCameraBack = false;
            cinemachine2.enabled = true;
        }
        else
        {
            PlayerData.Instance.isCameraBack = true;
            cinemachine2.enabled = false;
        }

    }
}
