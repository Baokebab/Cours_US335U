using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IA_Agent_Controller : MonoBehaviour
{

    #region Variables
    public bool showDebug = true;
    public bool isCheater = false;
    bool _cheaterRevealed = false;
    public Transform SchoolExit;
    [SerializeField] NavMeshAgent _navAgent;

    [SerializeField] float speedAnimation;
    Animator _animator;

    [SerializeField] AudioClip[] _audioHitList ;
    [SerializeField] AudioClip _cheaterLaugh;
    AudioSource _audioSource;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        speedAnimation = Random.Range(0.8f, 3f);
        _animator.speed = speedAnimation;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_navAgent.pathStatus == NavMeshPathStatus.PathComplete && _navAgent.remainingDistance <= 0.5f && _navAgent.remainingDistance > 0f)
        {
            Debug.Log(gameObject.name + " : Arrived at destination - " + SchoolExit.name);
            Destroy(gameObject);
        }

        if (Game_Manager._gameHasEnded)
        {
            GoToExit();
        }
    }
    //En cas de collision avec la craie
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out ChalkMovement chalkScript) && !chalkScript.hasTouchedIA)
        {
            chalkScript.hasTouchedIA = true;
            //Pour ne pas avoir de buffer sur l'animation
            if (!_animator.GetCurrentAnimatorStateInfo(1).IsName("Pain")) _animator.SetTrigger("isHurt");
        }        
    }
    public void HitSFX()
    {
        _audioSource.clip = _audioHitList[Random.Range(0, _audioHitList.Length)];
        _audioSource.Play();
    }
    public void CheaterHit()
    {
        if (isCheater)
        {
            RestoreAnimatorSpeed();
            _navAgent.enabled = true;
            _navAgent.SetDestination(SchoolExit.position);
            _animator.SetBool("isWalking", true);
            if (!_cheaterRevealed)
            {
                IA_Manager.sharedInstance.NbCheater--;
                _cheaterRevealed = true;
            }
        }
    }
    public void GoToExit()
    {
        _navAgent.enabled = true;
        _navAgent.SetDestination(SchoolExit.position);
        _animator.SetBool("isWalking", true);
    }
    public void CheaterLaughSFX()
    {
        RestoreAnimatorSpeed();
        _audioSource.clip = _cheaterLaugh;
        _audioSource.Play();
    }
    public void RestoreAnimatorSpeed()
    {
        _animator.speed = 1f;
    }
    public void SetAnimatorSpeed()
    {
        _animator.speed = speedAnimation;
    }
    //Draw NavMeshPath
    public void OnDrawGizmos() 
    {
        if (!showDebug)
            return;
        float height = _navAgent.height;
        if (_navAgent.hasPath)
        {
            Vector3[] corners = _navAgent.path.corners;
            if (corners.Length >= 2)
            {
                Gizmos.color = Color.red;
                for (int i = 1; i < corners.Length; i++)
                {
                    Gizmos.DrawLine(corners[i - 1] + Vector3.up * height / 2, corners[i] + Vector3.up * height / 2);
                }
            }
        }
    }
}
