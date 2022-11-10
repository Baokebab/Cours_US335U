using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalkMovement : MonoBehaviour
{
    [SerializeField] float ThrowForce = 200;
    public bool hasTouchedIA = false;
    [SerializeField] bool airBorne = true;
    [SerializeField] float angle;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * ThrowForce);
        rb.maxAngularVelocity = 70;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (airBorne) rb.AddTorque(transform.right * angle ); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        airBorne = false; //A modifier pour le bounce effect
        if(collision.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
        }
    }
}
