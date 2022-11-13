using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script pour faire des tests spécifiques
public class TestScript : MonoBehaviour
{
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 1.0f);
        Gizmos.DrawCube(transform.position, new Vector3( 0.8f, 0.6f , 2.4f));
    }
}
