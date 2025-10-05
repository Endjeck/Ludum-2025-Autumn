using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;           // скорость ходьбы
    public float runSpeed = 10f;           // скорость бега (ускорение)
    public float acceleration = 10f;       // скорость изменения скорости (ускорение)
    public float jumpForce = 7f;           // сила прыжка

    [Header("Ground Check Settings")]
    public LayerMask groundMask;            // слой земли для проверки касания
    public float groundCheckDistance = 0.1f; // расстояние проверки земли

    [Header("Mouse Rotation Settings")]
    public float mouseSensitivity = 5f;    // чувствительность мыши для вращения
    public Transform cameraTransform;      // ссылка на камеру (должна быть дочерним объектом или назначена в инспекторе)

    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 inputDirection;
    private bool isGrounded;

    private float rotationY = 0f;          // поворот персонажа по оси Y (влево-вправо)
    private float rotationX = 0f;          // поворот камеры по оси X (вверх-вниз)

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

        // Ускорение при нажатии Left Shift
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
        // Получаем движение мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Вращение персонажа по Y (влево-вправо)
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // Вращение камеры по X (вверх-вниз) с ограничением угла поворота
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void Move()
    {
        // Двигаем персонажа в направлении взгляда камеры, игнорируя вертикальную составляющую взгляда
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