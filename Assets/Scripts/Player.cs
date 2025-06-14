using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine {get; private set;}
    
    private PlayerIdleState _playerIdleState;
    private PlayerMoveState _playerMoveState;
    
    private void Awake()
    {
        StateMachine = new StateMachine();
        _playerIdleState = new PlayerIdleState(this, StateMachine, "Idle state");
        _playerMoveState = new PlayerMoveState(this, StateMachine, "Move state");
    }

    private void Start()
    {
        StateMachine.Initialize(_playerIdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
