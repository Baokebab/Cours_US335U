using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTester : MonoBehaviour
{
    public StateInfo FSMInfos = new StateInfo();
    public bool ShowDebug = false;
    [SerializeField] Animator animator;

    private FSMachine<StateBase, StateInfo> FSM = new FSMachine<StateBase, StateInfo>();


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        FSM.PeriodUpdate = 2;
        FSM.ShowDebug = ShowDebug;
        FSM.animator = animator;
        FSM.Update(FSMInfos);
    }
}

