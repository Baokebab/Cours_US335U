using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateLaughing : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        _animator.SetBool("isLaughing", true);
    }
}