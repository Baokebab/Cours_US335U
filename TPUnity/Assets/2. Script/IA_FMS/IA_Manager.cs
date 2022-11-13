using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using System;
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
    [SerializeField] int nbIas = 24;
    public int NbCheater = 2;
    [SerializeField] Transform _exitPos;
    [SerializeField] List<IA_Agent_Controller> IaList = new List<IA_Agent_Controller>();
    [SerializeField] Material[] _materialList;
    [SerializeField] Vector3[] _chairPos;
    [SerializeField] PaperPlaced[] _paperPlacement;


    IA_Agent_Controller[] _cheaterArray;
    Transform _player;

    public ReadOnlyCollection<IA_Agent_Controller> roIaList
    {
        get { return new ReadOnlyCollection<IA_Agent_Controller>(IaList); }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < nbIas; i++)
        {
            IA_Agent_Controller Ia = GameObject.Instantiate<IA_Agent_Controller>(prefabIA);
            Ia.transform.position = _chairPos[i];
            Ia.MyPaperPlacement = _paperPlacement[i];
            Ia.transform.parent = this.transform;
            Ia.SchoolExit = _exitPos;
            Ia.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = _materialList[UnityEngine.Random.Range(0, _materialList.Length)];
            Ia.transform.GetComponent<FSMTester>()._player = _player;
            IaList.Add(Ia);
        }
        _cheaterArray = IaList.OrderBy(x => Guid.NewGuid()).Take(NbCheater).ToArray();
        foreach(var cheater in _cheaterArray)
        {
            cheater.isCheater = true;
            cheater.GetComponent<FSMTester>().FSMInfos.cheatingRemaining = 6;
            //cheater.GetComponent<FSMTester>().FSMInfos.cheatingRemaining = UnityEngine.Random.Range(4, 7);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
