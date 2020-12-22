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
    public float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;

    public Vector2 value;
    public Vector3 value3;

    private Transform cameraMain;
    public Transform child;

    private void Awake()
    {
        player = new Player();
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
        Vector2 _movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        value = _movementInput;
        Vector3 _move = (cameraMain.forward * _movementInput.y + cameraMain.right * _movementInput.x);
        _move.y = 0f;
        value3 = _move;


         controller.Move(_move * Time.deltaTime * playerSpeed); // 움직임
         child.LookAt(child.position+ _move);
        
       

        
        playerVelocity.y += gravityValue * Time.deltaTime; //중력적용
        controller.Move(playerVelocity * Time.deltaTime); // 중력적용


        
    }
}
