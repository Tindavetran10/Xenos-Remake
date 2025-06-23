public class PlayerGroundedState : EntityState
{
    protected PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        if(PlayerRigidbody.linearVelocity.y < 0)
            StateMachine.ChangeState(Player.PlayerFallState);
        if (PlayerInputSet.Player.Jump.WasPressedThisFrame())
        {
            StateMachine.ChangeState(new PlayerJumpState(Player, StateMachine, "jumpfall"));
        }
    }
}