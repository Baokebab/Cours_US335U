using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNotFinished : FSMState<StateInfo>
{
    private float TempoTranquille = 0;
    private bool Init = true;
    public float PeriodNotCheating = Random.Range(6f, 11f);  //Tranquille pendant 6 à 11s
    public float PeriodCheating = Random.Range(3.5f, 6f); //Triche entre 3.5 et 6s

    public override void doState(ref StateInfo infos)
    {
        TempoTranquille += infos.PeriodUpdate;

        if ((TempoTranquille > PeriodCheating && isActiveSubstate<StateCheating>()) || Init)
        {
            TempoTranquille = 0;
            Init = false;
            PeriodCheating = Random.Range(3.5f, 6f);
            addAndActivateSubState<StateTranquille>();
        }
        //Triche uniquement si le prof n'est pas a côté et qu'il lui reste des triches
        else if (TempoTranquille > PeriodNotCheating && isActiveSubstate<StateTranquille>() && infos.cheatingRemaining > 0 && !infos.teacherIsClose)
        {
            TempoTranquille = 0;
            PeriodNotCheating = Random.Range(6f, 11f);
            addAndActivateSubState<StateCheating>();
            infos.cheatingRemaining--;
        }
        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }
}