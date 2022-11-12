using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IA_Agent_Controller : MonoBehaviour
{

    #region Variables
    public bool showDebug = true;
    public bool isCheater = true;
    [SerializeField] Transform SchoolExit;
    [SerializeField] NavMeshAgent navAgent;

    [SerializeField] float speedAnimation;
    Animator animator;

    [SerializeField] AudioClip[] _audioHitList ;
    AudioSource _audioSource;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        speedAnimation = Random.Range(0.8f, 3f);
        animator.speed = speedAnimation;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.pathStatus == NavMeshPathStatus.PathComplete && navAgent.remainingDistance <= 0.5f && navAgent.remainingDistance > 0f)
        {
            Debug.Log(gameObject.name + " : Arrived at destination - " + SchoolExit.name);
            Destroy(gameObject);
        }
    }

    //En cas de collision avec la craie
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out ChalkMovement chalkScript) && !chalkScript.hasTouchedIA)
        {
            animator.SetTrigger("isHurt");
            chalkScript.hasTouchedIA = true;
           
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
            if (SchoolExit != null)
            {
                navAgent.enabled = true;
                navAgent.SetDestination(SchoolExit.position);
                animator.SetBool("isWalking", true);
            }
            else Debug.Log(gameObject.name + " - Erreur : Il faut mettre le Transform de la porte de sortie.");
        }
    }
    public void RestoreAnimatorSpeed()
    {
        animator.speed = 1f;
    }
    public void SetAnimatorSpeed()
    {
        animator.speed = speedAnimation;
    }
  
    //Draw NavMeshPath
    public void OnDrawGizmos() 
    {
        if (!showDebug)
            return;
        float height = navAgent.height;
        if (navAgent.hasPath)
        {
            Vector3[] corners = navAgent.path.corners;
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
