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

    public float turnSpeed = 120f;
    public float acceleration = 8f;
    public float deceleration = 12f;  
    float currentSpeed = 0f;

    public float brakeStrength = 40f;

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
        Vector2 inputValue = input.Player.Move.ReadValue<Vector2>();
        float vertical = inputValue.y;
        float horizontal = inputValue.x;

        float throttle = Mathf.Clamp01(vertical);
        bool braking = vertical < 0f;

        float targetSpeed = throttle * moveSpeed;

        if (throttle > 0.01f)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                targetSpeed,
                acceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                deceleration * Time.fixedDeltaTime
            );
        }


        if (Mathf.Abs(horizontal) > 0.01f)
        {
            float turnAmount = horizontal * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0f, turnAmount, 0f);
        }

        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        moveDirection = forward * throttle;

        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            Vector3 dir = moveDirection.normalized;

            float halfHeight = (playerHeight * 0.5f) - wallRadius;
            if (halfHeight < 0f) halfHeight = 0f;

            Vector3 center = transform.position + Vector3.up * (playerHeight * 0.5f);

            Vector3 p1 = center + Vector3.up * halfHeight;
            Vector3 p2 = center - Vector3.up * halfHeight; 

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

        float mult = grounded ? 1f : airMultiplier;

        if (moveDirection.sqrMagnitude > 0.0001f && currentSpeed > 0.1f)
        {
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f * mult, ForceMode.Force);
        }


        if (braking && grounded)
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            float speed = flatVel.magnitude;

            if (speed > 0.1f)
            {
                Vector3 brakeDir = -flatVel.normalized;
                rb.AddForce(brakeDir * brakeStrength, ForceMode.Acceleration);
            }
        }
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

    private void OnDestroy()
    {
        input.Player.Disable();
        input.UI.Disable();
    }
}
