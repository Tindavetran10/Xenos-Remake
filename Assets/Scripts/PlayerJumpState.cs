public class PlayerJumpState : EntityState
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
        if (PlayerRigidbody.linearVelocity.y < 0)
        {
            StateMachine.ChangeState(Player.PlayerFallState);
        }
    }
}