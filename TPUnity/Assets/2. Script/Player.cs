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
    float _verticalRotation = 0f;
    [SerializeField] float _downAngle, _upAngle;
    Animator animator;
    Camera camPov;
    
    [SerializeField] AudioClip[] _audioList;
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camPov = Camera.main;
    }

    void FixedUpdate()
    {
        Move();
        Fire();
    }

    private void Update()
    {
        Look();
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
        if(!Game_Manager._gameIsPaused)
        {
            Vector2 rotationInput = ControllerManager.rotationInput;

            if (rotationInput != Vector2.zero)
            {
                transform.Rotate(Vector3.up, rotationInput.x * Time.deltaTime * sensiHorizontale);
                _verticalRotation -= rotationInput.y * Time.deltaTime * sensiVerticale;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _upAngle, _downAngle);
                camPov.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
            }
        }
    }

    //Player Left Click
    void Fire()
    {
        if(ControllerManager.leftClick && !Game_Manager._gameIsPaused)
        {
            ControllerManager.leftClick = false;
            animator.SetTrigger("hasThrowed");
            Instantiate(ChalkPrefab, camPov.transform.position, camPov.transform.rotation);
            //if(!_audioSource.isPlaying) //Pour éviter de spammer de couper un son déjà en play
            //{
            //    _audioSource.clip = _audioList[Random.Range(0, _audioList.Length)];
            //    _audioSource.Play();
            //}
           
            //A modif pour mettre au nb d'IA plutôt
            BoidManager.sharedInstance.boids[Random.Range(0, BoidManager.sharedInstance.boids.Count)].isDead();
        }
    }


}
