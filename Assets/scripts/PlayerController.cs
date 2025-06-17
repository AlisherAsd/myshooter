using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 7f; // Сила прыжка
    public float groundCheckDistance = 0.1f; // Дистанция проверки земли
    public LayerMask groundLayer; // Слой, который считается землей
    
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Поворот мышью
        float rotateY = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotateY, 0);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Проверка, стоит ли игрок на земле
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 
                     groundCheckDistance, groundLayer);

        // Движение WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);
    }

    // Визуализация луча для отладки (видно в Scene View)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, 
                        transform.position + Vector3.down * groundCheckDistance);
    }
}