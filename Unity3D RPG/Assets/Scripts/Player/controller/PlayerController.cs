using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private MainPlayer playerInput;
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 3.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;

    public Vector2 value;
    public Vector3 value3;
    public float distance;

    private Transform cameraMain;
    public Transform child;


    private float angularVelocity = 0f;
    float angle;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerInput = new MainPlayer();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void Start()
    {
        cameraMain = Camera.main.transform;
        child = transform.GetChild(0).transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (player.isDash == false)
        {   
            Vector2 _movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
            value = _movementInput;
            
            Vector3 _dis = new Vector3(_movementInput.x, 0f, _movementInput.y);
            distance = Vector3.Magnitude(_dis);

            Vector3 _move = (cameraMain.forward * _movementInput.y + cameraMain.right * _movementInput.x);
            _move.y = 0f;
            value3 = _move;


            if (player.isFight == false && player.isGuard == false)
            {
                if(_move != Vector3.zero)
                {
                    ////child.LookAt(child.position + _move);
                    float a = Mathf.Atan2(_move.x, _move.z) * Mathf.Rad2Deg;
                    angle = Mathf.SmoothDampAngle(child.eulerAngles.y, a, ref angularVelocity, 0.08f);
                    child.rotation = Quaternion.Euler(0f, angle, 0f);

                    controller.Move(_move * Time.deltaTime * playerSpeed); // 움직임
                }

                //실패작들
                //Vector3 G = child.position - _move;
                //float a = Mathf.SmoothDampAngle(child.eulerAngles.x,G.x , ref angularVelocity, 0.3f);
                //float b = Mathf.SmoothDampAngle(child.eulerAngles.y,G.y , ref angularVelocity, 0.3f);
                //float c = Mathf.SmoothDampAngle(child.eulerAngles.z,G.z , ref angularVelocity, 0.3f);
                //child.localEulerAngles = new Vector3(a, b, c) ;
                
                //var zAngle : float = Mathf.SmoothDampAngle(transform.localEulerAngles.z, _rollTowards, zVelocity, smooth * Time.deltaTime);
                //var targetRot = Quaternion.LookRotation(child.position + _move);
                //var delta = Quaternion.Angle(child.rotation, targetRot);
                //if (delta > 0f)
                //{

                //    var t = Mathf.SmoothDampAngle(delta, 0f, ref angularVelocity, 0.1f);
                //    t = 1 / delta;
                //    child.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);
                //}
                //child.rotation = Quaternion.Lerp(child.rotation, Quaternion.LookRotation(child.position + _move), 1000f * Time.deltaTime);
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime; //중력적용
        controller.Move(playerVelocity * Time.deltaTime); // 중력적용
    }
}
