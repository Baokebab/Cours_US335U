using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTranquille : FSMState<StateInfo>
{
    public float PeriodSitting = Random.Range(2.0f,5f);  //Assis à rien faire entre 3 et 5s
    public float PeriodWriting = Random.Range(5.0f,10f); //Ecris entre 5 et 10s
    private float TempoTranquille = 0;
    private bool Init = true;


    public override void doState(ref StateInfo infos)
    {
        TempoTranquille += infos.PeriodUpdate;

        if ((TempoTranquille > PeriodSitting && isActiveSubstate<StateIdleSitting>()) || Init) 
        {
            Init = false;
            TempoTranquille = 0;
            PeriodSitting = Random.Range(2.0f, 5f);
            addAndActivateSubState<StateWriting>();
            infos.writingRemaining--;
        }
        else if (TempoTranquille > PeriodWriting && isActiveSubstate<StateWriting>())
        {
            TempoTranquille = 0;
            PeriodWriting = Random.Range(5.0f, 10f);
            addAndActivateSubState<StateIdleSitting>();
        }
        KeepMeAlive = true; //Sinon on perds la tempo, l'init etc...
    }
}