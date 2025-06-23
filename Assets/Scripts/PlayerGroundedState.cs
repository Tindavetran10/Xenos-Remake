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
        
        // Because the Raycast of GroundDetected is slightly longer than the Player's sprite,
        // Therefore, the Player is already switched to Grounded State before the Player's sprite change, So
        //  we can also use the Raycast to register the Jump Input just before the Player's sprite change
        if (PlayerInputSet.Player.Jump.WasPressedThisFrame() || Player.HasJumpBuffered() && Player.CanJump())
        {
            Player.ConsumeJumpBuffer();
            Player.ConsumeCoyoteTime();
            StateMachine.ChangeState(Player.PlayerJumpState);
        }
    }
}