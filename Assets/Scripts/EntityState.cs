// This will be a base class for all character's states in the game

using UnityEngine;

public abstract class EntityState 
{
    protected Player Player;
    protected StateMachine StateMachine;
    protected string AnimBoolName;
    
    protected Animator PlayerAnimator;
    protected Rigidbody2D PlayerRigidbody;
    
    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        AnimBoolName = animBoolName;
        
        PlayerAnimator = Player.Animator;
        PlayerRigidbody = Player.Rb;
    }

    public virtual void Enter()
    {
        // Everytime, the current state will be changed. Enter will be called
        //Debug.Log("I enter " + AnimBoolName);
        PlayerAnimator.SetBool(AnimBoolName, true);
    }

    public virtual void Update()
    {
        // We're going to run the logic of the state here
        //Debug.Log("I run update of" + AnimBoolName);
    }

    public virtual void Exit()
    {
        // This will be called everytime we exit the state and change to a new one
        //Debug.Log("I exit" + AnimBoolName);
        PlayerAnimator.SetBool(AnimBoolName, false);
    }
}
