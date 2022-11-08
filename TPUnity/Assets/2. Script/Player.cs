using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 inputDirection;
    [SerializeField] GameObject ChalkPrefab;
    [SerializeField] float playerSpeed = 500;
    [SerializeField] float sensiHorizontale = 8f;
    [SerializeField] float sensiVerticale = 0.5f;
    Animator animator;
    Camera camPov;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        camPov = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Look();
        Fire();
    }

    //Player Movement ZQSD
    void Move()
    {
        Vector2 moveInput = ControllerManager.moveInput;
        if (moveInput != Vector2.zero)
        {
            inputDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
            rb.velocity = inputDirection * playerSpeed * Time.fixedDeltaTime;
            animator.SetBool("isWalking",true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("isWalking", false);
        }
    }


    //Rotation souris/camera
    void Look()
    {
        Vector2 rotationInput = ControllerManager.rotationInput;
        float limitCamX = camPov.transform.eulerAngles.x;
        if (rotationInput != Vector2.zero)
        {
            transform.Rotate(Vector3.up, rotationInput.x * Time.fixedDeltaTime * sensiHorizontale);
            camPov.transform.Rotate(Vector3.left, rotationInput.y * Time.fixedDeltaTime * sensiVerticale);
            if ((rotationInput.y <0 && ((limitCamX >= 0 && limitCamX < 69f) || (limitCamX <= 360f && limitCamX >= 180f)))
                || (rotationInput.y > 0 && ((limitCamX >= 0 && limitCamX <= 180f) || (limitCamX <= 360f && limitCamX > 313f))))
            {
                
            }  
        }
    }

    //Player Left Click
    void Fire()
    {
        if(ControllerManager.leftClick)
        {
            animator.SetTrigger("hasThrowed");
            Instantiate(ChalkPrefab, camPov.transform.position, camPov.transform.rotation);
            ControllerManager.leftClick = false;
        }
    }


}
