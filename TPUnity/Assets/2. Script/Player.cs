using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    #region Variables
    Rigidbody _rb;
    Vector3 _inputDirection;
    [SerializeField] GameObject _chalkPrefab;
    [SerializeField] GameObject _paperPrefab;
    [SerializeField] float _playerSpeed = 50;
    [SerializeField] float _sensiHorizontale = 25f;
    [SerializeField] float _sensiVerticale = 0.5f;
    float _verticalRotation = 0f;
    [SerializeField] float _downAngle, _upAngle;
    Animator _animator;
    Camera _camPov;

    [SerializeField] AudioClip[] _audioList;
    [SerializeField] AudioClip _startVoice;
    int _audioIndex = 0;
    AudioSource _audioSource;

    #endregion
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _camPov = Camera.main;
        _audioList = _audioList.OrderBy(x => System.Guid.NewGuid()).ToArray();
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
            _inputDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
            _rb.velocity = _inputDirection * _playerSpeed * Time.fixedDeltaTime;
            _animator.SetBool("isWalking",true);
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _animator.SetBool("isWalking", false);
        }
    }

    public void YouCanStartVoice()
    {
        _audioSource.clip = _startVoice;
        _audioSource.Play();
    }
    //Rotation souris/camera
    void Look()
    {
        if(!Game_Manager.GameIsPaused)
        {
            Vector2 rotationInput = ControllerManager.rotationInput;

            if (rotationInput != Vector2.zero)
            {
                transform.Rotate(Vector3.up, rotationInput.x * Time.deltaTime * _sensiHorizontale);
                _verticalRotation -= rotationInput.y * Time.deltaTime * _sensiVerticale;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _upAngle, _downAngle);
                _camPov.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
            }
        }
    }

    //Player Left Click
    void Fire()
    {
        if(ControllerManager.leftClick && !Game_Manager.GameIsPaused)
        {
            _animator.SetTrigger("hasThrowed");
            ControllerManager.leftClick = false;

            if (Game_Manager.PartGame == 0)
            {
                Instantiate(_paperPrefab, _camPov.transform.position, _camPov.transform.rotation);
                _audioSource.clip = _audioList[_audioIndex++];
                _audioSource.Play();
                if(_audioIndex == _audioList.Length)
                {
                    _audioList = _audioList.OrderBy(x => System.Guid.NewGuid()).ToArray();
                    _audioIndex = 0;
                }
            }
            if (Game_Manager.PartGame == 1)
            {
                Instantiate(_chalkPrefab, _camPov.transform.position, _camPov.transform.rotation);
                //A modif pour une intéraction plus intéressante peut etre
                BoidManager.sharedInstance.boids[Random.Range(0, BoidManager.sharedInstance.boids.Count)].isDead();
            }
        }
    }


}
