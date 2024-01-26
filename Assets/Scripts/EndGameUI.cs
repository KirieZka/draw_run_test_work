using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public GameObject endGamePanel;
    public Text resultText;
    public Button restartButton;
    public Button nextLevelButton;

    private void Start()
    {
        // Скрыть панель окончания игры при старте
        endGamePanel.SetActive(false);

        // Привязать методы к событиям кнопок
        restartButton.onClick.AddListener(RestartGame);
        nextLevelButton.onClick.AddListener(GoToNextLevel);
    }

    // Метод для отображения окна окончания игры
    public void ShowEndGameUI(bool victory)
    {
        endGamePanel.SetActive(true);

        if (victory)
        {
            resultText.text = "Победа!";
            // Дополнительные действия при победе
        }
        else
        {
            resultText.text = "Поражение";
            // Дополнительные действия при поражении
        }
    }

    // Метод для перезапуска уровня
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Метод для перехода к следующему уровню (если нужно)
    private void GoToNextLevel()
    {
        // Реализуйте логику перехода к следующему уровню, если необходимо
        // Пример: SceneManager.LoadScene("СледующийУровень");
    }
}
