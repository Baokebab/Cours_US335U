using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateLaughing : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        animator.SetBool("isLaughing", true);
        Debug.Log("Je suis en train de rigoler !");
    }
}