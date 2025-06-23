using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    
    public PlayerInputSet Input { get; private set; }
    public StateMachine StateMachine {get; private set;}
    
    public PlayerIdleState PlayerIdleState {get; private set;}
    public PlayerMoveState PlayerMoveState {get; private set;}

    public PlayerJumpState PlayerJumpState {get; private set;}
    public PlayerFallState PlayerFallState {get; private set;}
    
    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce;
    private bool _isFacingRight = true;
    
    public Vector2 MoveInput {get; private set; }
    
    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        
        StateMachine = new StateMachine();
        Input = new PlayerInputSet();
        
        PlayerIdleState = new PlayerIdleState(this, StateMachine, "idle");
        PlayerMoveState = new PlayerMoveState(this, StateMachine, "move");
        PlayerJumpState = new PlayerJumpState(this, StateMachine, "jumpfall");
        PlayerFallState = new PlayerFallState(this, StateMachine, "jumpfall");
    }

    private void OnEnable()
    {
        Input.Enable();
        
        //_input.Player.Movement.started - trigger when we begin to press the button
        //_input.Player.Movement.performed - trigger when we hold or fully press the button
        //_input.Player.Movement.canceled - trigger when we release the button

        Input.Player.Movement.performed += context => MoveInput = context.ReadValue<Vector2>();
        Input.Player.Movement.canceled += context => MoveInput = Vector2.zero;
    }

    private void OnDisable() => Input.Disable();

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
