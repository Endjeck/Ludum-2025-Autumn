using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;           // �������� ������
    public float runSpeed = 10f;           // �������� ���� (���������)
    public float acceleration = 10f;       // �������� ��������� �������� (���������)
    public float jumpForce = 7f;           // ���� ������

    [Header("Ground Check Settings")]
    public LayerMask groundMask;            // ���� ����� ��� �������� �������
    public float groundCheckDistance = 0.1f; // ���������� �������� �����

    [Header("Mouse Rotation Settings")]
    public float mouseSensitivity = 5f;    // ���������������� ���� ��� ��������
    public Transform cameraTransform;      // ������ �� ������ (������ ���� �������� �������� ��� ��������� � ����������)

    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 inputDirection;
    private bool isGrounded;

    private float rotationY = 0f;          // ������� ��������� �� ��� Y (�����-������)
    private float rotationX = 0f;          // ������� ������ �� ��� X (�����-����)

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        currentSpeed = walkSpeed;

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned! Please assign it in inspector.");
        }
    }

    private void Update()
    {
        GetInput();
        CheckGround();
        HandleJump();
        HandleRotation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        // ��������� ��� ������� Left Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundMask);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void HandleRotation()
    {
        // �������� �������� ����
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // �������� ��������� �� Y (�����-������)
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // �������� ������ �� X (�����-����) � ������������ ���� ��������
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void Move()
    {
        // ������� ��������� � ����������� ������� ������, ��������� ������������ ������������ �������
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDirection = forward * inputDirection.z + right * inputDirection.x;
        moveDirection = moveDirection.normalized;

        Vector3 targetVelocity = moveDirection * currentSpeed;
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);

        float maxChange = acceleration * Time.fixedDeltaTime;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxChange, maxChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxChange, maxChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}