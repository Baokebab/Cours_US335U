public class StateBase : FSMState<StateInfo>
{
    public override void doState(ref StateInfo infos)
    {
        if (infos.writingRemaining > 0)
            addAndActivateSubState<StateNotFinished>();
        else
            addAndActivateSubState<StateIdleSitting>();

        KeepMeAlive = true;
    }
}