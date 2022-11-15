using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PaperMovement : MonoBehaviour
{
    public float ThrowForce = 400;
    public bool airBorne = true;
    [SerializeField] float angle;
    public Rigidbody rb;
    public BoxCollider BoxCollider;
    bool _isPlaced = false;
    bool _isMarked = false;
    public TextMeshProUGUI textPaper;
    Vector3 _PosUrgence;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * ThrowForce); 
        rb.maxAngularVelocity = 60;
        BoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (airBorne) rb.AddTorque(transform.up * angle);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player")) airBorne = false; //A modifier pour le bounce effect
        if (collision.gameObject.CompareTag("Floor")) //A modif si on veut laisser au sol
        {
            if(_isPlaced)
            {
                transform.position = _PosUrgence;
            }
            else
            {
                Destroy(this.gameObject);
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PaperPlacement"))
        {
            if(!other.GetComponent<PaperPlaced>().paperPlaced && !_isPlaced)
            {
                _isPlaced = true;
                rb.velocity = Vector3.zero;
                _PosUrgence = other.transform.position ;
                transform.position = _PosUrgence;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                other.GetComponent<PaperPlaced>().paperPlaced = true;
                other.GetComponent<PaperPlaced>().myPaper = this;
                PaperPlacement.PaperPlacedList.Add(this);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_isMarked &&  collision.gameObject.CompareTag("StepMark") && Mathf.Abs(rb.velocity.magnitude) < 0.01f)
        {
            PaperPlacement.ListMark[Int32.Parse(collision.gameObject.name)]++;
            _isMarked = true;
        }
    }

}
