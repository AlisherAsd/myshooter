using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public GameObject gameDiePanel;
    public GameObject gamePanel; // Предполагается, что здесь Image

    [SerializeField] private TextMeshProUGUI healthText; // Поле для ссылки на UI текст

    private Image gamePanelImage; // Для управления цветом
    private Color originalColor;

    void Start()
    {
        // Обновляем текст при старте игры
        UpdateHealthDisplay();

        // Получаем компонент Image и сохраняем оригинальный цвет
        if (gamePanel != null)
        {
            gamePanelImage = gamePanel.GetComponent<Image>();
            if (gamePanelImage != null)
            {
                originalColor = gamePanelImage.color;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Игрок получил урон! Осталось здоровья: " + health);
        UpdateHealthDisplay();

        // Запускаем вспышку
        if (gamePanelImage != null)
        {
            StartCoroutine(FlashRed());
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "" + health;
        }
    }

    void Die()
    {
        Debug.Log("Игрок мёртв!");
        gameDiePanel.SetActive(true);
        FindObjectOfType<DieMenuManager>()?.ShowDieMenu(); 
    }

    System.Collections.IEnumerator FlashRed()
    {
        // Меняем цвет на красный с полупрозрачностью
        gamePanelImage.color = new Color(1, 0, 0, 0.5f);

        // Ждём 0.1 секунды
        yield return new WaitForSeconds(0.2f);

        // Возвращаем оригинальный цвет
        gamePanelImage.color = originalColor;
    }
}