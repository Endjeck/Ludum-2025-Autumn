using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;           // скорость ходьбы
    public float runSpeed = 10f;           // скорость бега (ускорение)
    public float acceleration = 10f;       // скорость изменения скорости (ускорение)
    public float jumpForce = 7f;            // сила прыжка

    [Header("Ground Check Settings")]
    public LayerMask groundMask;            // слой земли для проверки касания
    public float groundCheckDistance = 0.1f; // расстояние проверки земли

    [Header("Mouse Rotation Settings")]
    public float mouseSensitivity = 5f;    // чувствительность мыши для вращения

    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 inputDirection;
    private bool isGrounded;

    private float rotationY = 0f;           // текущий поворот по Y

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        // Разрешаем вращение только по Y вручную
        currentSpeed = walkSpeed;
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
        // Проверяем, стоит ли персонаж на земле с помощью луча вниз
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
        // Получаем движение мыши по горизонтали (движение вправо/влево)
        float mouseX = Input.GetAxis("Mouse X");

        // Увеличиваем текущий поворот по Y на смещение мыши с чувствительностью
        rotationY += mouseX * mouseSensitivity;

        // Применяем поворот только по оси Y (вверх-вниз ось не трогаем)
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    private void Move()
    {
        // Двигаем персонажа в локальных координатах с учетом поворота
        // Чтобы движение было направлено согласно взгляду персонажа, преобразуем inputDirection из локальных в мировые координаты
        Vector3 moveDirection = transform.TransformDirection(inputDirection);

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