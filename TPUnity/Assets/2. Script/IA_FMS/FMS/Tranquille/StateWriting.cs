using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWriting : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        animator.SetBool("isLaughing", false);
        animator.SetBool("isCheating", false);
        animator.SetBool("isWriting", true);
        Debug.Log("J'écris...");
    }

}