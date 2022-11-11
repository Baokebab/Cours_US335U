using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHasToCheat : FSMState<StateInfo>
{

    public float PeriodIsCheating = 8;
    private float TempoIsCheating = 0;
    private bool Init = true;


    public override void doState(ref StateInfo infos)
    {
        TempoIsCheating += infos.PeriodUpdate;

        if (TempoIsCheating > PeriodIsCheating || Init)
        {
            TempoIsCheating = 0;
            Init = false;
            if (isActiveSubstate<StateCheating>())
            {
                addAndActivateSubState<StateLaughing>(); 
            }
            else
            {
                addAndActivateSubState<StateCheating>();
            }
            infos.cheatingRemaining--;
            infos.writingRemaining -= 3;
        }

        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }
}