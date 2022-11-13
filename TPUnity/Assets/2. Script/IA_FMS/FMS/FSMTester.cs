using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMTester : MonoBehaviour
{
    public StateInfo FSMInfos = new StateInfo();
    public bool ShowDebug = false;
    Animator _animator;
    IA_Agent_Controller _IaController;
    NavMeshAgent _IaNavAgent;
    public Transform _player;
    private FSMachine<StateBase, StateInfo> FSM = new FSMachine<StateBase, StateInfo>();


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _IaController = GetComponent<IA_Agent_Controller>();
        _IaNavAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        FSM.PeriodUpdate = 2;
        FSM.ShowDebug = ShowDebug;
        FSM._animator = _animator;
        FSM._IaController = _IaController;
        FSM._IaNavAgent = _IaNavAgent;
        FSM.Update(FSMInfos);
        if (Vector3.Distance(_player.position, transform.position) < FSMInfos.distanceClose) FSMInfos.teacherIsClose = true;
        else FSMInfos.teacherIsClose = false;
    }
}

