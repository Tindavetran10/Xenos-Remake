public class PlayerMoveState : EntityState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        if(Player.MoveInput.x == 0)
            StateMachine.ChangeState(Player.PlayerIdleState);
        
        Player.SetVelocity(Player.MoveInput.x * Player.moveSpeed, PlayerRigidbody.linearVelocity.y);
    }
}