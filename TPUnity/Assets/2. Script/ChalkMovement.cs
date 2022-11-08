using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalkMovement : MonoBehaviour
{
    [SerializeField] float ThrowForce = 200;
    // Start is called before the first frame update
    void Start()
    {
       this.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
        }
    }
}
