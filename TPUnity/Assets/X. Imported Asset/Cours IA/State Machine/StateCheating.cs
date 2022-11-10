using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateCheating : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        animator.SetBool("isCheating", true);
        Debug.Log("Je suis en train de tricher !");
    }
}