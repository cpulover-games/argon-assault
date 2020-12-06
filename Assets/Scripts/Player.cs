using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private InputMaster controls;

    [SerializeField] private float xSpeed = 25f;
    private const float X_BOUNDARY = 11.5f;

    [SerializeField] private float ySpeed = 25f;
    private const float Y_BOUNDARY = 11.5f;


    private void Awake()
    {
        controls = new InputMaster();
        //controls.Player.Move.performed += Move;
    }

    private void OnDestroy()
    {
        controls.Dispose();
        //controls.Player.Movement.performed -= context => Move(context.ReadValue<float>()); //neccessary?
    }

    private void Move(InputAction.CallbackContext context)
    {
        print("Player moving " + context.ReadValue<float>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();

        float nextX = transform.localPosition.x + moveInput.x * xSpeed * Time.deltaTime;
        nextX = Mathf.Clamp(nextX, -X_BOUNDARY, X_BOUNDARY);

        float nextY = transform.localPosition.y + moveInput.y * ySpeed * Time.deltaTime;
        nextY = Mathf.Clamp(nextY, -Y_BOUNDARY, Y_BOUNDARY);

        transform.localPosition = new Vector3(nextX, nextY, transform.localPosition.z);
    }
}

