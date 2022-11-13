using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHasToCheat : FSMState<StateInfo>
{
    private float TempoIsCheating = 0;
    public float PeriodOnPhone = Random.Range(3.0f, 5f);  //Regarde sur son tel pendant 3 à 5s
    public float PeriodLaughing = 3f; //Rigole pendant 1.5secondes
    private bool Init = true;


    public override void doState(ref StateInfo infos)
    {
        TempoIsCheating += infos.PeriodUpdate;
        if ((TempoIsCheating > PeriodLaughing && isActiveSubstate<StateLaughing>()) || Init)
        {
            Init = false;
            TempoIsCheating = 0;
            addAndActivateSubState<StateCheating>();
            infos.cheatingRemaining--;
            infos.writingRemaining -= 3;
        }
        else if (TempoIsCheating > PeriodOnPhone && isActiveSubstate<StateCheating>())
        {
            TempoIsCheating = 0;
            addAndActivateSubState<StateLaughing>();
        }

        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }
}