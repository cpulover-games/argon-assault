using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private InputMaster controls;

    [SerializeField] private float xSpeed = 25f;
    [SerializeField] private float ySpeed = 25f;

    private const float X_MIN_POSITION = -21.5f;
    private const float X_MAX_POSITION = 21.5f;
    private const float Y_MIN_POSITION = -12f;
    private const float Y_MAX_POSITION = 14f;
    private const float X_MIN_ROTATION = -45f; 
    private const float X_MAX_ROTATION = 0f; 
    private const float Y_MIN_ROTATION = -25f; 
    private const float Y_MAX_ROTATION = 25f; 

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
        ProcessPositioning();
        ProcessRotating();
    }

    private float MapInterval(float val, float srcFrom, float srcTo, float dstFrom, float dstTo)
    {
        return dstFrom + (val - srcFrom) / (srcTo - srcFrom) * (dstTo - dstFrom);
    }

    private void ProcessRotating()
    {
        // x rotation increases when y position decreases
        float nextXRotation = MapInterval(transform.localPosition.y, Y_MAX_POSITION, Y_MIN_POSITION, X_MIN_ROTATION, X_MAX_ROTATION);
        // y rotation increases when x position increases
        float nextYRotation = MapInterval(transform.localPosition.x, X_MIN_POSITION, X_MAX_POSITION, Y_MIN_ROTATION, Y_MAX_ROTATION);

        transform.localRotation = Quaternion.Euler(nextXRotation, nextYRotation, 0);
    }

    private void ProcessPositioning()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();

        float nextXPosition = transform.localPosition.x + moveInput.x * xSpeed * Time.deltaTime;
        nextXPosition = Mathf.Clamp(nextXPosition, X_MIN_POSITION, X_MAX_POSITION);

        float nextYPosition = transform.localPosition.y + moveInput.y * ySpeed * Time.deltaTime;
        nextYPosition = Mathf.Clamp(nextYPosition, Y_MIN_POSITION, Y_MAX_POSITION);

        transform.localPosition = new Vector3(nextXPosition, nextYPosition, transform.localPosition.z);
    }

    private void OnPlayerCollided() //called by string reference
    {
        controls.Disable();
    }
}

