using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateCheating : FSMState<StateInfo>
{
    private float TempoNotSafe = 0;
    public float PeriodNotSafe = 2;  //S'il triche pendant 2s alors que le prof est à côté -> se remet en tranquille
    public float PeriodCheating = Random.Range(3.5f, 6f); //Triche entre 4 et 6s


    public override void doState(ref StateInfo infos)
    {

        _animator.SetBool("isCheating", true);
        if (infos.teacherIsClose)
        {
            TempoNotSafe += infos.PeriodUpdate;
            if(TempoNotSafe > PeriodNotSafe)
            {
                addAndActivateSubState<StateTranquille>();
            }
        }
        else
        {
            TempoNotSafe = 0;
        }

    }
}