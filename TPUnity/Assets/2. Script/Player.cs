using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour
{

    #region Variables
    Rigidbody _rb;
    Vector3 _inputDirection;
    public bool CanPlay = false;
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
    NavMeshAgent _navAgent;
    [SerializeField] Transform _step0;
    public static int Health = 2;

    #endregion
    private void Awake()
    {
        Health = 2;
    }
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _camPov = Camera.main;
        _audioList = _audioList.OrderBy(x => System.Guid.NewGuid()).ToArray();
    }

    void FixedUpdate()
    {
        if(CanPlay)
        {
            Move();
            Fire();
        }    
    }

    private void Update()
    {
        if(CanPlay)
        {
            Look();
        }     
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
        Game_Manager.sharedInstance._BackgroundMusic.clip = Game_Manager.sharedInstance._backGroundListMusic[1];
        Game_Manager.sharedInstance._BackgroundMusic.Play();
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
                if (_audioIndex == _audioList.Length)
                {
                    _audioList = _audioList.OrderBy(x => System.Guid.NewGuid()).ToArray();
                    _audioIndex = 0;
                }
            }
            else if (Game_Manager.PartGame == 1)
            {
                Instantiate(_chalkPrefab, _camPov.transform.position, _camPov.transform.rotation);
                BoidManager.sharedInstance.boids[Random.Range(0, BoidManager.sharedInstance.boids.Count)].isDead(); //A modif pour une intéraction plus intéressante peut etre si y'a le time
                _audioSource.clip = _audioList[_audioIndex++];
                _audioSource.Play();
                if (_audioIndex == _audioList.Length)
                {
                    _audioList = _audioList.OrderBy(x => System.Guid.NewGuid()).ToArray();
                    _audioIndex = 0;
                }
            }
            else if (Game_Manager.PartGame == 2 && Game_Manager.CurrentPaperMarked < PaperPlacement.PaperPlacedList.Count)
            {
                PaperMovement currentPaper = PaperPlacement.PaperPlacedList[Game_Manager.CurrentPaperMarked];
                currentPaper.rb.constraints = RigidbodyConstraints.None;
                currentPaper.BoxCollider.enabled = true;
                currentPaper.airBorne = true;
                currentPaper.transform.position = _camPov.transform.position;
                currentPaper.transform.rotation = _camPov.transform.rotation ;
                currentPaper.rb.AddForce(currentPaper.transform.forward * currentPaper.ThrowForce);
                currentPaper.rb.useGravity = true;
                Game_Manager.CurrentPaperMarked++;
            }
        }
    }

    public async UniTask GoToLastStage()
    {
        _navAgent.enabled = true;
        _navAgent.SetDestination(_step0.position);
        while (Vector3.Distance(transform.position, _step0.position) > 1f)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(1));
        }
        _navAgent.enabled = false;
    }

}
