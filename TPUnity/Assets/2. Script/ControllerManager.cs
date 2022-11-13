using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ControllerManager : MonoBehaviour
{
    public static Vector2 moveInput, rotationInput;
    public static bool leftClick = false, EscapeKey = false;
    void Start()
    {
        
    }
    public void OnFire()
    {
        leftClick = true;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        rotationInput = value.Get<Vector2>();
    }

    public void OnMenu()
    {
        EscapeKey = true;
    }

}
