public class PlayerIdleState : EntityState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        if(Player.MoveInput.x != 0)
            StateMachine.ChangeState(Player.PlayerMoveState);
    }
}