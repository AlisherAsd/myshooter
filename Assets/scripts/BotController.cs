using UnityEngine;

public class MoveInRandomDirection : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxDistance = 100.0f;
    public Transform player;
    public float shootInterval = 5f;
    public float shootRange = 100f;
    public float shootHeightOffset = 1.0f;
    public int damagePerShot = 10;

    // Звуковые параметры
    [SerializeField] private AudioClip shootSound; // Звук выстрела
    [SerializeField] private AudioSource audioSource; // Источник звука

    private Vector3 direction;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        direction = GetRandomDirection();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Автоматически добавит, если не задан
        }

        InvokeRepeating(nameof(ShootAtPlayer), shootInterval, shootInterval);
    }

    void Update()
    {
        if (CheckForObstacles(0.5f))
        {
            direction = GetRandomDirection();
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (player != null)
        {
            FacePlayer();
        }

        if (Vector3.Distance(transform.position, startPosition) > maxDistance)
        {
            direction = GetRandomDirection();
        }
    }

    void FacePlayer()
    {
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lookDirection),
                Time.deltaTime * 5f
            );
        }
    }

    void ShootAtPlayer()
    {
        if (player == null) return;
        Debug.Log("Пытаюсь стрелять. Игрок: " + (player != null));

        Vector3 shootPosition = transform.position + Vector3.up * shootHeightOffset;
        Vector3 shootDirection = (player.position + Vector3.up * shootHeightOffset) - shootPosition;
        float distanceToPlayer = shootDirection.magnitude;
        shootDirection.Normalize();

        // Проверяем, есть ли прямая видимость до игрока
        if (HasLineOfSight(shootPosition, shootDirection, distanceToPlayer))
        {
            Debug.DrawRay(shootPosition, shootDirection * distanceToPlayer, Color.red, 1f);

            RaycastHit hit;
            if (Physics.Raycast(shootPosition, shootDirection, out hit, shootRange))
            {
                if (hit.collider.transform == player)
                {
                    Debug.Log("Попали в игрока!");
                    PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damagePerShot);
                    }
                }
            }

            // Проигрываем звук выстрела
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    bool HasLineOfSight(Vector3 from, Vector3 direction, float distance)
    {
        RaycastHit hit;
        int obstacleLayer = LayerMask.GetMask("Obstacle");
        int playerLayer = LayerMask.GetMask("Player");
        int layerMask = obstacleLayer | playerLayer;

        if (Physics.Raycast(from, direction, out hit, distance, layerMask))
        {
            // Если попали в игрока - есть видимость
            if (hit.collider.transform == player)
            {
                return true;
            }
            // Если попали в препятствие - видимости нет
            return false;
        }
        // Если ничего не попало - считаем что видимость есть
        return true;
    }

    bool CheckForObstacles(float checkDistance)
    {
        return Physics.Raycast(transform.position, direction, checkDistance);
    }

    Vector3 GetRandomDirection()
    {
        return new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;
    }
}