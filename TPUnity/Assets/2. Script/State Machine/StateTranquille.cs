using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTranquille : FSMState<StateInfo>
{
    public float PeriodIdle = 5;
    private float TempoIdle = 0;
    private bool Init = true;

    public override void doState(ref StateInfo infos)
    {
        TempoIdle += infos.PeriodUpdate;

        if (TempoIdle > PeriodIdle || Init)
        {
            TempoIdle = 0;
            Init = false;
            if (isActiveSubstate<StateIdleSitting>())
            {
                addAndActivateSubState<StateWriting>();
                infos.writingRemaining--;
            }
            else
            {
                addAndActivateSubState<StateIdleSitting>();
            }
        }

        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }
}