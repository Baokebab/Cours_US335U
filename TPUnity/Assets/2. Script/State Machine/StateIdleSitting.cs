using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleSitting : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        animator.SetBool("isLaughing", false);
        animator.SetBool("isCheating", false);
        animator.SetBool("isWriting", false);
        Debug.Log("Je fais une petite anim d'idle assis");
    }
}