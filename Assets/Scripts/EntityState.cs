// This will be a base class for all character's states in the game
using UnityEngine;

public abstract class EntityState 
{
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    protected Player Player;
    protected StateMachine StateMachine;
    protected string AnimBoolName;
    
    protected Animator PlayerAnimator;
    protected Rigidbody2D PlayerRigidbody;
    protected PlayerInputSet PlayerInputSet;

    protected EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        AnimBoolName = animBoolName;
        
        PlayerAnimator = Player.Animator;
        PlayerRigidbody = Player.Rb;
        PlayerInputSet = Player.Input;
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
        
        // This will handle the switching between Jump and Fall BlendTree from Player's AC
        PlayerAnimator.SetFloat(YVelocity, PlayerRigidbody.linearVelocity.y);
    }

    public virtual void Exit()
    {
        // This will be called everytime we exit the state and change to a new one
        //Debug.Log("I exit" + AnimBoolName);
        PlayerAnimator.SetBool(AnimBoolName, false);
    }
}
