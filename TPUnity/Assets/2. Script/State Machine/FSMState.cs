using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState<StateInfo> where StateInfo : FSMStateInfo
{
    public string Name = "Undefined";
    public bool ShowDebug = false;
    public bool KeepMeAlive = false; //Si on doit me détruire à la fin de ma mise à jour
    public Animator animator;

    public List<FSMState<StateInfo>> SubStates = new List<FSMState<StateInfo>>();
    public FSMState<StateInfo> ActiveSubState = null;
    private static int RecursionLevel = 0; //Pour afficher correctement la niveau hiérarchique de l'état lors du debug.

    public FSMState()
    {
        Name = GetType().ToString();
    }

    public void Update(ref StateInfo infos)
    {
        RecursionLevel++;

        log("Update");

        KeepMeAlive = false; //Je dois explicitement demander à rester en vie dans mon doState

        doState(ref infos);

        //Si on a pas de SubState actif, on dépile
        if (ActiveSubState == null)
        {
            if (SubStates.Count > 0)
                ActiveSubState = SubStates[SubStates.Count - 1]; //On prend le dernier à s'être activé
        }

        //Si on a un substate actif, on l'exécute
        if (ActiveSubState != null)
        {
            ActiveSubState.ShowDebug = this.ShowDebug;
            ActiveSubState.animator = this.animator;
            ActiveSubState.Update(ref infos);
            if (!ActiveSubState.KeepMeAlive)
            {
                removeSubState(ActiveSubState);
                log(ActiveSubState.Name + " has ended");
            }
        }

        //Si je n'ai pas demandé à rester en vie mais qu'un de mes enfants, actif ou non le veut, alors je dois rester en vie
        foreach (FSMState<StateInfo> state in SubStates)
            if (state.KeepMeAlive)
                KeepMeAlive = true;

        RecursionLevel--;
    }

    //Pour le comportement de l'état
    public abstract void doState(ref StateInfo infos);

    public bool isActiveSubstate<State>() where State : FSMState<StateInfo>
    {
        if (ActiveSubState != null && ActiveSubState.GetType() == typeof(State))
            return true;
        return false;
    }

    FSMState<StateInfo> findSubStateWithType<State>() where State : FSMState<StateInfo>
    {
        foreach (FSMState<StateInfo> state in SubStates)
            if (state.GetType() == typeof(State))
                return state;
        return null;
    }

    protected void addAndActivateSubState<State>() where State : FSMState<StateInfo>, new()
    {
        FSMState<StateInfo> state = findSubStateWithType<State>();
        if (state == null)
        {
            state = new State();
            log("Create " + state.Name);
        }
        else
        {
            SubStates.Remove(state);
        }

        //Ajouté en dernière position
        SubStates.Add(state);
        ActiveSubState = state;
    }

    protected void clearSubStates()
    {
        log("Cleared substates");
        SubStates.Clear();
        ActiveSubState = null;
    }

    protected void removeSubState<State>() where State : FSMState<StateInfo>, new()
    {
        FSMState<StateInfo> state = findSubStateWithType<State>();
        if (state != null)
        {
            log("Remove " + state.Name);
            SubStates.Remove(state);
        }
        if (ActiveSubState == state)
            ActiveSubState = null;
    }

    protected void removeSubState(FSMState<StateInfo> state)
    {
        if (SubStates.Remove(state))
        {
            log("Remove " + state.Name);
        }
    }

    protected void log(string message)
    {
        if (!ShowDebug)
            return;
        string msg = "";
        for (int i = 0; i < RecursionLevel; i++)
        {
            msg += "-";
        }
        Debug.Log(msg + " [" + Name + "] " + message);
    }
}