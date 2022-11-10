using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNotFinished : FSMState<StateInfo>
{
    public float PeriodTranquille = 5;
    private float TempoTranquille = 0;
    private bool Init = true;

    public override void doState(ref StateInfo infos)
    {
        TempoTranquille += infos.PeriodUpdate;

        if ((TempoTranquille > PeriodTranquille || Init))
        {
            TempoTranquille = 0;
            Init = false;
            if (isActiveSubstate<StateTranquille>() && infos.cheatingRemaining > 0)
            {
                addAndActivateSubState<StateHasToCheat>();
            }
            else
            {
                addAndActivateSubState<StateTranquille>();
            }
        }

        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }
}