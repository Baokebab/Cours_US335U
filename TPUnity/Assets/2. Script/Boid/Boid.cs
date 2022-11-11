using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float zoneRepulsion = 5;
    public float zoneAlignement = 7;
    public float zoneAttraction = 50;

    public float forceRepulsion = 30;
    public float forceAlignement = 30;
    public float forceAttraction = 30;

    public Vector3 target = new Vector3();
    public float forceTarget = 20;
    public bool goToTarget = false;

    public Vector3 velocity = new Vector3();
    public float maxSpeed = 20;
    public float minSpeed = 12;

    public bool drawGizmos = true;
    public bool drawLines = true;

    public float SmoothingRotation = 0.5f;
    public float AquaX = 0.4f, AquaY = 0.3f, AquaZ = 1.2f;
    public Transform CenterOfFishTank;
    public GameObject BubbleFX;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CenterOfFishTank = transform.parent.GetChild(0).transform;
        target = CenterOfFishTank.position + new Vector3(Random.Range(-AquaX, AquaX), Random.Range(-AquaY, AquaY), Random.Range(-AquaZ, AquaZ));
        animator.speed = Random.Range(0.5f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 sumForces = new Vector3();
        Color colorDebugForce = Color.black;
        float nbForcesApplied = 0;

        foreach (Boid otherBoid in BoidManager.sharedInstance.roBoids)
        {
            Vector3 vecToOtherBoid = otherBoid.transform.position - transform.position;

            Vector3 forceToApply = new Vector3();

            //Si on doit prendre en compte cet autre boid (plus grande zone de perception)
            if (vecToOtherBoid.sqrMagnitude < zoneAttraction * zoneAttraction)
            {
                //Si on est entre attraction et alignement
                if (vecToOtherBoid.sqrMagnitude > zoneAlignement * zoneAlignement)
                {
                    //On est dans la zone d'attraction uniquement
                    forceToApply = vecToOtherBoid.normalized * forceAttraction;
                    float distToOtherBoid = vecToOtherBoid.magnitude;
                    float normalizedDistanceToNextZone = ((distToOtherBoid - zoneAlignement) / (zoneAttraction - zoneAlignement));
                    float boostForce = (4 * normalizedDistanceToNextZone);
                    if (!goToTarget) //Encore plus de cohésion si pas de target
                        boostForce *= boostForce;
                    forceToApply = vecToOtherBoid.normalized * forceAttraction * boostForce;
                    colorDebugForce += Color.green;
                }
                else
                {
                    //On est dans alignement, mais est on hors de répulsion ?
                    if (vecToOtherBoid.sqrMagnitude > zoneRepulsion * zoneRepulsion)
                    {
                        //On est dans la zone d'alignement uniquement
                        forceToApply = otherBoid.velocity.normalized * forceAlignement;
                        colorDebugForce += Color.blue;
                    }
                    else
                    {
                        //On est dans la zone de repulsion
                        float distToOtherBoid = vecToOtherBoid.magnitude;
                        float normalizedDistanceToPreviousZone = 1 - (distToOtherBoid / zoneRepulsion);
                        float boostForce = (4 * normalizedDistanceToPreviousZone);
                        forceToApply = vecToOtherBoid.normalized * -1 * (forceRepulsion * boostForce);
                        colorDebugForce += Color.red;

                    }
                }

                sumForces += forceToApply;
                nbForcesApplied++;
            }
        }

        //On fait la moyenne des forces, ce qui nous rend indépendant du nombre de boids
        sumForces /= nbForcesApplied;

        //Si on a une target, on l'ajoute
        if (goToTarget)
        {
            Vector3 vecToTarget = target - transform.position;
            if (vecToTarget.sqrMagnitude < 0.1f)
            {
                target = CenterOfFishTank.position + new Vector3(Random.Range(-AquaX, AquaX), Random.Range(-AquaY, AquaY), Random.Range(-AquaZ, AquaZ));
                goToTarget = false;
            }
                
            else
            {
                Vector3 forceToTarget = vecToTarget.normalized * forceTarget;
                sumForces += forceToTarget;
                colorDebugForce += Color.magenta;
                nbForcesApplied++;
                if (drawLines)
                    Debug.DrawLine(transform.position, target, Color.magenta);
            }
        }

        //Debug
        if (drawLines)
            Debug.DrawLine(transform.position, transform.position + sumForces, colorDebugForce / nbForcesApplied);

        //On freine
        velocity += -velocity * 10 * Vector3.Angle(sumForces, velocity) / 180.0f * Time.deltaTime;

        //on applique les forces
        velocity += sumForces * Time.deltaTime;

        //On limite la vitesse
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
            velocity = velocity.normalized * maxSpeed;
        if (velocity.sqrMagnitude < minSpeed * minSpeed)
            velocity = velocity.normalized * minSpeed;

        //On regarde dans la bonne direction        
        //if (velocity.sqrMagnitude > 0)
        //    transform.LookAt(transform.position + velocity);

        //Rotation plus smooth que celle ci-dessus
        Vector3 dirToTarget = (target - transform.position);
        dirToTarget.y = 0;
        Quaternion rot = Quaternion.FromToRotation(transform.forward, dirToTarget.normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot * transform.rotation, 1.0f - Mathf.Pow(Mathf.Clamp01(SmoothingRotation), Time.deltaTime));


        //Debug
        if (drawLines)
            Debug.DrawLine(transform.position, transform.position + velocity, Color.blue);

        //Deplacement du boid
        transform.position += velocity * Time.deltaTime;
    }

    public void isDead()
    {
        animator.SetBool("isDead", true);
        BubbleFX.SetActive(false);
        maxSpeed = minSpeed+0.001f;
    }

    void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            // Répulsion
            Gizmos.color = new Color(1, 0, 0, 1.0f);
            Gizmos.DrawWireSphere(transform.position, zoneRepulsion);
            // Alignement
            Gizmos.color = new Color(0, 1, 0, 1.0f);
            Gizmos.DrawWireSphere(transform.position, zoneAlignement);
            // Attraction
            Gizmos.color = new Color(0, 0, 1, 1.0f);
            Gizmos.DrawWireSphere(transform.position, zoneAttraction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "SM_Prop_Fishtank_FX_01")
        {
            goToTarget = true;
        }
    }
}