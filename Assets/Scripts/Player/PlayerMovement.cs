using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions input;
    public Transform cam;

    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public float coyoteTime = 0.15f;
    float coyoteTimer = 0f;

    public float walkSpeed;
    public float sprintSpeed;

    public LayerMask whatIsGround;
    public bool grounded;
    public float groundCheckRadius = 0.35f;
    public float groundCheckOffset = 0.1f;

    public Transform orientation;

    public float wallCheckDistance = 0.6f;
    public float wallRadius = 0.3f;
    public float maxWallAngle = 80f;
    public float playerHeight = 2f;
    public LayerMask wallMask = ~0;

    Vector3 moveDirection;
    Rigidbody rb;

    float jumpCooldownTimer = 0f;

    private void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GroundCheck();

        // coyote time
        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // input salto
        MyInput();

        // cooldown del salto
        if (!readyToJump)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
                readyToJump = true;
        }

        SpeedControl();

        // drag
        rb.linearDamping = grounded ? groundDrag : 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GroundCheck()
    {
        Vector3 origin = transform.position + Vector3.up * groundCheckOffset;

        grounded = Physics.CheckSphere(
            origin,
            groundCheckRadius,
            whatIsGround,
            QueryTriggerInteraction.Ignore
        );
    }

    private void MyInput()
    {
        if (input.Player.Jump.WasPressedThisFrame() && readyToJump && coyoteTimer > 0f)
        {
            readyToJump = false;
            jumpCooldownTimer = jumpCooldown;

            Jump();
        }
    }

    private void MovePlayer()
    {
        // dirección basada en la cámara
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector2 inputValue = input.Player.Move.ReadValue<Vector2>();
        moveDirection = forward * inputValue.y + right * inputValue.x;

        // rotar hacia donde nos movemos
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // wall slide
        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            Vector3 dir = moveDirection.normalized;

            float halfHeight = (playerHeight * 0.5f) - wallRadius;
            if (halfHeight < 0f) halfHeight = 0f;

            Vector3 center = transform.position + Vector3.up * (playerHeight * 0.5f);

            Vector3 p1 = center + Vector3.up * halfHeight; // arriba
            Vector3 p2 = center - Vector3.up * halfHeight; // abajo

            if (Physics.CapsuleCast(p1, p2, wallRadius, dir, out RaycastHit hit,
                wallCheckDistance, wallMask, QueryTriggerInteraction.Ignore))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);
                if (angle > maxWallAngle)
                {
                    moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal);
                }
            }
        }

        // aplicar fuerza de movimiento
        float mult = grounded ? 1f : airMultiplier;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * mult, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
