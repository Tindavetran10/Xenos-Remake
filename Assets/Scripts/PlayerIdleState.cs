public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetVelocity(0, PlayerRigidbody.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        
        if(Player.MoveInput.x != 0)
            StateMachine.ChangeState(Player.PlayerMoveState);
    }
}