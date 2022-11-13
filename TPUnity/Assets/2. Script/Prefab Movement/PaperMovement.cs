using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMovement : MonoBehaviour
{
    [SerializeField] float ThrowForce = 200;
    [SerializeField] bool airBorne = true;
    [SerializeField] float angle;
    public Rigidbody rb;
    public BoxCollider BoxCollider;
    bool _isPlaced;

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
            Destroy(this.gameObject);
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
                transform.position = other.transform.position;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                other.GetComponent<PaperPlaced>().paperPlaced = true;
                other.GetComponent<PaperPlaced>().myPaper = this;
                PaperPlacement.PaperPlacedSet.Add(this);
            }
        }
    }
}
