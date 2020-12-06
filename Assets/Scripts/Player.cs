using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private InputMaster controls;

    [SerializeField] private float xSpeed = 20f;
    private const float X_BOUNDARY = 11f;

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
        float horizontalInput = controls.Player.Move.ReadValue<float>();
        float nextX = transform.localPosition.x + horizontalInput * xSpeed * Time.deltaTime;
        nextX = Mathf.Clamp(nextX, -X_BOUNDARY, X_BOUNDARY); 
        transform.localPosition = new Vector3(nextX, transform.localPosition.y, transform.localPosition.z);      
    }
}

