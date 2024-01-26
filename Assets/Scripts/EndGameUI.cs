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
        // ������ ������ ��������� ���� ��� ������
        endGamePanel.SetActive(false);

        // ��������� ������ � �������� ������
        restartButton.onClick.AddListener(RestartGame);
        nextLevelButton.onClick.AddListener(GoToNextLevel);
    }

    // ����� ��� ����������� ���� ��������� ����
    public void ShowEndGameUI(bool victory)
    {
        endGamePanel.SetActive(true);

        if (victory)
        {
            resultText.text = "������!";
            // �������������� �������� ��� ������
        }
        else
        {
            resultText.text = "���������";
            // �������������� �������� ��� ���������
        }
    }

    // ����� ��� ����������� ������
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ����� ��� �������� � ���������� ������ (���� �����)
    private void GoToNextLevel()
    {
        // ���������� ������ �������� � ���������� ������, ���� ����������
        // ������: SceneManager.LoadScene("����������������");
    }
}
