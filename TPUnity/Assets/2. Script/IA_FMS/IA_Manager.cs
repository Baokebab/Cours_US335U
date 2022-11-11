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

    [SerializeField] Material[] _materialList;
    [SerializeField] IA_Agent_Controller prefabIA;
    [SerializeField] float nbIAs = 1;
    [SerializeField] List<IA_Agent_Controller> IaList = new List<IA_Agent_Controller>();


    public ReadOnlyCollection<IA_Agent_Controller> roIaList
    {
        get { return new ReadOnlyCollection<IA_Agent_Controller>(IaList); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
