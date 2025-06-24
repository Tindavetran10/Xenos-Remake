public class PlayerGroundedState : EntityState
{
    protected PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        // Prevent Player staying in Idle State when falling
        if(PlayerRigidbody.linearVelocity.y < 0 && !Player.GroundDetected)
            StateMachine.ChangeState(Player.PlayerFallState);
        
        if (PlayerInputSet.Player.Jump.WasPressedThisFrame() && Player.CanJump())
        {
            Player.ConsumeJumpBuffer();
            Player.ConsumeCoyoteTime();
            StateMachine.ChangeState(Player.PlayerJumpState);
        }
    }
}