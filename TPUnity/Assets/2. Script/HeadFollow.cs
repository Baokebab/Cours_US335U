using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollow : MonoBehaviour
{

    [SerializeField] Transform[] Bones;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        doCCD(target.position, 0.5f ) ;
    }

    private void doCCD(Vector3 targetPos, float blending)
    {
        for (int i = 1; i < Bones.Length; i++)
        {
            Vector3 directionActuelle = (Bones[0].position - Bones[i].position).normalized;
            Vector3 directionDesiree = (targetPos - Bones[i].position).normalized;
            Quaternion rotation = Quaternion.FromToRotation(directionActuelle, directionDesiree.normalized);
            Bones[i].rotation = Quaternion.Lerp(Bones[i].rotation, (rotation) * Bones[i].rotation, blending);
        }
    }
}
