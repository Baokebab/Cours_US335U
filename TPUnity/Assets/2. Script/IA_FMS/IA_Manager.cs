using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class IA_Manager : MonoBehaviour
{
    private static IA_Manager instance = null;
    public static IA_Manager sharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<IA_Manager>();
            }
            return instance;
        }
    }

    #region Variables

    [SerializeField] IA_Agent_Controller prefabIA;
    [SerializeField ]float nbIas = 1;
    [SerializeField] Transform _exitPos;
    [SerializeField] List<IA_Agent_Controller> IaList = new List<IA_Agent_Controller>();
    [SerializeField] Material[] _materialList;
    [SerializeField] Vector3[] _chairPos;


    public ReadOnlyCollection<IA_Agent_Controller> roIaList
    {
        get { return new ReadOnlyCollection<IA_Agent_Controller>(IaList); }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbIas; i++)
        {
            IA_Agent_Controller Ia = GameObject.Instantiate<IA_Agent_Controller>(prefabIA);
            Ia.transform.position = _chairPos[i];
            Ia.transform.parent = this.transform;
            Ia.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = _materialList[Random.Range(0, _materialList.Length)];
            IaList.Add(Ia);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
