using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Camera playerCamera; // Камера игрока
    public float maxDistance = 100f; // Максимальная дистанция стрельбы
    public GameObject Click;

    [SerializeField] private AudioClip shotSound; // Поле для аудиоклипа выстрела
    [SerializeField] private AudioSource audioSource; // Для проигрывания звука

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(0))
        {
            Debug.Log("Клик мыши зарегистрирован");

            // Включаем объект Click и прячем его через время
            Click.SetActive(true);
            StartCoroutine(HideClickAfterDelay(0.2f));

            // Проигрываем звук выстрела
            if (audioSource != null && shotSound != null)
            {
                audioSource.PlayOneShot(shotSound);
            }

            // Выпускаем луч из центра экрана
            Ray ray = playerCamera != null ? 
                playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)) : 
                Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                Debug.Log("Луч попал в объект: " + hit.collider.name);

                ShotBotController bot = hit.collider.GetComponent<ShotBotController>();
                if (bot != null)
                {
                    Debug.Log("Найден компонент ShotBotController");
                    bot.OnHit();
                }
                else
                {
                    Debug.LogWarning("Объект имеет коллайдер, но компонент ShotBotController отсутствует");
                }
            }
            else
            {
                Debug.Log("Луч не попал ни во что");
            }
        }
    }

    System.Collections.IEnumerator HideClickAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ждём заданное время
        Click.SetActive(false); // Выключаем объект
    }
}