using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerController playerController;
    private Vector2 move;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float startingMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    private void Awake()
    {
        base.Awake();
        playerController = new PlayerController();
        myTrailRenderer.emitting = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        playerController.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    private void OnEnable()
    {
        playerController.Enable();
    }

    private void Update()
    {
        AdjustPlayerFacingDirection();
        PlayerInput();
    }
    private void FixedUpdate()
    {
        
        Move();
    }
    private void PlayerInput()
    {
        move = playerController.Movement.Move.ReadValue<Vector2>();
        animator.SetFloat("moveX", move.x);
        animator.SetFloat("moveY", move.y);
    }
    private void Move()
    {
        rb.MovePosition(rb.position+move*(moveSpeed*Time.fixedDeltaTime));
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }
}
