using UnityEngine;

public class DieMenuManager : MonoBehaviour
{
    public GameObject gamePanel; // Основная игровая панель (HUD)
    public GameObject diePanel;  // Панель смерти (должна быть отключена в начале)

    // Ссылки на компоненты, которые нужно отключать
    private PlayerMovement playerMovement;
    private ShotBotController shotBotController;
    private MoveInRandomDirection moveInRandomDirection;

    private void Start()
    {
        // Получаем ссылки на компоненты
        playerMovement = FindObjectOfType<PlayerMovement>();
        moveInRandomDirection = FindObjectOfType<MoveInRandomDirection>();
        shotBotController = FindObjectOfType<ShotBotController>();

        // Отключаем все игровые скрипты при старте
        DisableAllScripts();

        if (diePanel != null)
            diePanel.SetActive(false); // Скрываем панель смерти
    }

    // Вызывается при смерти игрока
    public void ShowDieMenu()
    {
        if (diePanel == null)
        {
            Debug.LogError("Ссылка на панель смерти не установлена!");
            return;
        }

        Debug.Log("Показываем меню смерти");
        diePanel.SetActive(true); // Включаем панель
        if (gamePanel != null)
            gamePanel.SetActive(false);
        
        DisableAllScripts(); // Отключаем управление
    }

    

    // Вызывается при нажатии кнопки "Restart"
    public void StartGame()
    {
        if (gamePanel == null)
        {
            Debug.LogError("Ссылка на игровую панель не установлена!");
            return;
        }

        Debug.Log("Игра началась!");
        gamePanel.SetActive(true); // Включаем игровую панель
        diePanel.SetActive(false); // Скрываем панель смерти
        
        EnableAllScripts(); // Включаем управление
    }

    // Отключает все скрипты
    private void DisableAllScripts()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;

        if (moveInRandomDirection != null)
            moveInRandomDirection.enabled = false;

        if (shotBotController != null)
            shotBotController.enabled = false;
            
    }

    // Включает все скрипты
    private void EnableAllScripts()
    {
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (moveInRandomDirection != null)
            moveInRandomDirection.enabled = true;

            
        if (shotBotController != null)
            shotBotController.enabled = true;
    
    }
}