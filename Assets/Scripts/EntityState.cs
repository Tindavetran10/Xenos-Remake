// This will be a base class for all character's states in the game

using UnityEngine;

public class EntityState 
{
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        // Everytime, the current state will be changed. Enter will be called
        Debug.Log(" I enter" + stateName);
    }

    public virtual void Update()
    {
        // We're going to run the logic of the state here
        Debug.Log(" I run update of" + stateName);
    }

    public virtual void Exit()
    {
        // This will be called everytime we exit the state and change to a new one
        Debug.Log(" I exit" + stateName);
    }
}
