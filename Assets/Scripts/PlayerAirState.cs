public class PlayerAirState : EntityState
{
    public PlayerAirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    
    public override void Update()
    {
        base.Update();
        
        if(Player.MoveInput.x != 0)
            Player.SetVelocity(Player.MoveInput.x * Player.moveSpeed * Player.inAirMultiplier, PlayerRigidbody.linearVelocity.y);

        if (PlayerInputSet.Player.Jump.WasPerformedThisFrame() || Player.HasJumpBuffered() && Player.HasCoyoteTime())
        {
            Player.ConsumeJumpBuffer();
            Player.ConsumeCoyoteTime();
            StateMachine.ChangeState(Player.PlayerJumpState);
        }
    }
    
    
    
}