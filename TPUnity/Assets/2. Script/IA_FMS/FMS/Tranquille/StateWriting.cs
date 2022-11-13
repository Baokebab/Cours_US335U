using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWriting : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        _animator.SetBool("isLaughing", false);
        _animator.SetBool("isCheating", false);
        _animator.SetBool("isWriting", true);
    }

}