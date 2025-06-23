using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components and References
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    #endregion

    #region Input and State Management
    public PlayerInputSet Input { get; private set; }
    public StateMachine StateMachine { get; private set; }
    
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }
    public PlayerJumpState PlayerJumpState { get; private set; }
    public PlayerFallState PlayerFallState { get; private set; }
    
    public Vector2 MoveInput { get; private set; }
    #endregion

    #region Movement Configuration
    [Header("Movement details")]
    public float moveSpeed;
    private bool _isFacingRight = true;
    [Range(0F ,1F)] public float inAirMultiplier;
    
    [Header("Jump details")]
    public float jumpForce;
    public float jumpCutMultiplier;
    [SerializeField] private float jumpBufferWindow = 0.2f; //How long the jump buffer last
    private float _jumpBufferTimer; // Countdown the timer for buffered input
    #endregion

    #region Collision Detection
    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool GroundDetected { get; private set; }
    #endregion

    #region Unity Life Cycle
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

        Input.Player.Jump.performed += context => _jumpBufferTimer = jumpBufferWindow;
    }

    private void OnDisable() => Input.Disable();

    private void Start() => StateMachine.Initialize(PlayerIdleState);

    private void Update()
    {
        HandleCollisionDetection();
        
        if(_jumpBufferTimer > 0f) 
            _jumpBufferTimer -= Time.deltaTime;
        
        StateMachine.UpdateActiveState();
        
    }

    #endregion

    #region Movement Methods
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipHandler(xVelocity);
    }
    #endregion

    #region Utility Methods
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

    private void HandleCollisionDetection() => 
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

    private void OnDrawGizmos() => Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));

    #endregion
    
    #region Helper Method
    public bool HasJumpBuffered() => _jumpBufferTimer > 0f;
    public void ConsumeJumpBuffer() => _jumpBufferTimer = 0f;
    #endregion
}