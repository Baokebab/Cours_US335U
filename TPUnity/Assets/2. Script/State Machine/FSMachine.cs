using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMachine<State, StateInfo>  where State : FSMState<StateInfo>, new() where StateInfo : FSMStateInfo
{
    private State BaseState;
    public float PeriodUpdate = 0.3f;
    public bool ShowDebug;
    private float TempoUpdate = 0;
    public Animator animator;

    public void Update(StateInfo infos)
    {
        if (BaseState == null)
        {
            BaseState = new State();
            return;
        }

        TempoUpdate += Time.deltaTime;
        if (TempoUpdate > PeriodUpdate)
        {
            TempoUpdate = 0;
            infos.PeriodUpdate = PeriodUpdate;
            BaseState.ShowDebug = ShowDebug;
            BaseState.animator = animator;
            BaseState.Update(ref infos);
        }
    }
}