public class PlayerAirState : EntityState
{
    protected PlayerAirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    
    public override void Update()
    {
        base.Update();
        
        if(Player.MoveInput.x != 0)
            Player.SetVelocity(Player.MoveInput.x * Player.moveSpeed * Player.inAirMultiplier, PlayerRigidbody.linearVelocity.y);

        // When the Player is in the air, allow him to make a jump if he has coyote time and jump buffer
        if (PlayerInputSet.Player.Jump.WasPerformedThisFrame() || Player.HasJumpBuffered() || Player.CanJump())
        {
            Player.ConsumeJumpBuffer();
            Player.ConsumeCoyoteTime();
            StateMachine.ChangeState(Player.PlayerJumpState);
        }
    }
    
    
    
}