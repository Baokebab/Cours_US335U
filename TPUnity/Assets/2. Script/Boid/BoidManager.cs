using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    private static BoidManager instance = null;
    public static BoidManager sharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BoidManager>();
            }
            return instance;
        }
    }

    public Boid prefabBoid;
    public float nbBoids = 100;
    public float startSpeed = 1;
    public float startSpread = 10;
    public Transform CenterOfFishTank;
    public float maxDistBoids = 30;

    public float periodRetargetBoids = 6;
    public float periodNoTargetBoids = 3;
    private float timerRetargetBoids = 0;
    private bool setTargetToBoids = true;
    //public float AquaX = 0.7f, AquaY = 0.55f, AquaZ = 1.46f;

    public List<Boid> boids = new List<Boid>();
    public ReadOnlyCollection<Boid> roBoids
    {
        get { return new ReadOnlyCollection<Boid>(boids); }
    }

    void Start()
    {
        for (int i = 0; i < nbBoids; i++)
        {
            Boid b = GameObject.Instantiate<Boid>(prefabBoid);
            Vector3 positionBoid = CenterOfFishTank.position + Random.insideUnitSphere * startSpread;
            b.transform.position = positionBoid;
            b.velocity = (positionBoid - transform.position).normalized * startSpeed;
            b.transform.parent = this.transform;
            b.maxSpeed *= Random.Range(0.95f, 1.05f);
            //b.target = CenterOfFishTank.position + new Vector3(Random.Range(-AquaX, AquaX), Random.Range(-AquaY, AquaY), Random.Range(-AquaZ, AquaZ));
            boids.Add(b);
        }
    }

    void Update()
    {
        timerRetargetBoids -= Time.deltaTime;
        if (timerRetargetBoids <= 0)
        {
            if (!setTargetToBoids)
                timerRetargetBoids = periodNoTargetBoids;
            else
                timerRetargetBoids = periodRetargetBoids;

            //Vector3 target = CenterOfFishTank.position + new Vector3(Random.Range(-AquaX, AquaX), Random.Range(-AquaY, AquaY), Random.Range(-AquaZ, AquaZ));
            foreach (Boid b in boids)
            {
                //b.goToTarget = false;
                if (Vector3.Distance(CenterOfFishTank.position, transform.position) < startSpread && setTargetToBoids && Random.Range(0.0f, 1.0f) < 0.3f)
                {
                    //b.target = target;
                    //b.goToTarget = true;
                    b.goToTarget = false;
                }
                else
                {
                    b.goToTarget = true;
                }
            }

            setTargetToBoids = !setTargetToBoids;
        }
    }
}