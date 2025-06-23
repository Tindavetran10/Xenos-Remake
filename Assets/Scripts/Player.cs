using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    
    private PlayerInputSet _input;
    public StateMachine StateMachine {get; private set;}
    
    public PlayerIdleState PlayerIdleState {get; private set;}
    public PlayerMoveState PlayerMoveState {get; private set;}

    public Vector2 MoveInput {get; private set; }
    
    [Header("Movement details")]
    public float moveSpeed;
    
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        
        StateMachine = new StateMachine();
        _input = new PlayerInputSet();
        
        PlayerIdleState = new PlayerIdleState(this, StateMachine, "idle");
        PlayerMoveState = new PlayerMoveState(this, StateMachine, "move");
    }

    private void OnEnable()
    {
        _input.Enable();
        
        //_input.Player.Movement.started - trigger when we begin to press the button
        //_input.Player.Movement.performed - trigger when we hold or fully press the button
        //_input.Player.Movement.canceled - trigger when we release the button

        _input.Player.Movement.performed += context => MoveInput = context.ReadValue<Vector2>();
        _input.Player.Movement.canceled += context => MoveInput = Vector2.zero;
    }

    private void OnDisable() => _input.Disable();

    private void Start() => StateMachine.Initialize(PlayerIdleState);

    private void Update() => StateMachine.UpdateActiveState();

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipHandler(xVelocity);
    }


    private void FlipHandler(float xVelocity)
    {
        // if the player moving right and not facing right / facing left,
        // Flip the player
        // if the player moving left and not facing left / facing right,
        // Flip the player
        
        if(xVelocity > 0 && _isFacingRight == false)
            Flip();
        else if(xVelocity < 0 && _isFacingRight)
            Flip();
    }
    
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        _isFacingRight = !_isFacingRight;
    }
}
