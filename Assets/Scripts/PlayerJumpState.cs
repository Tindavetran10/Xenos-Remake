public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        // Make object go up, increase Y velocity
        Player.SetVelocity(PlayerRigidbody.linearVelocity.x, Player.jumpForce);
        
    }

    public override void Update()
    {
        base.Update();
        
        // Jump Cut: If the player releases the jump button while still ascending, reduce velocity
        if (!PlayerInputSet.Player.Jump.IsPressed() && PlayerRigidbody.linearVelocity.y > 0)
            Player.SetVelocity(PlayerRigidbody.linearVelocity.x, 
                PlayerRigidbody.linearVelocity.y * Player.jumpCutMultiplier);
        
        if (PlayerRigidbody.linearVelocity.y < 0) 
            StateMachine.ChangeState(Player.PlayerFallState);
    }
}