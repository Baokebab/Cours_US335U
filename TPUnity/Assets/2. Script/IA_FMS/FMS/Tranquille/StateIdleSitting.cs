using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateIdleSitting : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        _animator.SetBool("isLaughing", false);
        _animator.SetBool("isCheating", false);
        _animator.SetBool("isWriting", false);
    }
}