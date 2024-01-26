using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text crystalsText;  // UI-текст для отображения числа кристаллов
    public Text runnersText;   // UI-текст для отображения числа бегунов
    public DrawingPanel drawingPanel;  // Ссылка на ваш объект DrawingPanel

    void Start()
    {
        // Проверка, что DrawingPanel указан
        if (drawingPanel == null)
        {
            Debug.LogError("DrawingPanel не указан в UIManager. Присоедините объект DrawingPanel через инспектор.");
        }
    }

    void Update()
    {
        // Обновление числа кристаллов в тексте
        UpdateCrystalsText();

        // Обновление числа бегунов в тексте
        UpdateRunnersText();
    }

    void UpdateCrystalsText()
    {
        // Проверка, что UI-текст для кристаллов указан
        if (crystalsText != null)
        {
            // Обновление текста с числом кристаллов
            crystalsText.text = "Crystals: " + drawingPanel.CrystalsCollected.ToString();
        }
    }

    void UpdateRunnersText()
    {
        // Проверка, что UI-текст для бегунов указан
        if (runnersText != null)
        {
            // Обновление текста с числом бегунов
            runnersText.text = "Runners: " + drawingPanel.RunnersCount.ToString();
        }
    }
}
