using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text crystalsText;  // UI-����� ��� ����������� ����� ����������
    public Text runnersText;   // UI-����� ��� ����������� ����� �������
    public DrawingPanel drawingPanel;  // ������ �� ��� ������ DrawingPanel

    void Start()
    {
        // ��������, ��� DrawingPanel ������
        if (drawingPanel == null)
        {
            Debug.LogError("DrawingPanel �� ������ � UIManager. ������������ ������ DrawingPanel ����� ���������.");
        }
    }

    void Update()
    {
        // ���������� ����� ���������� � ������
        UpdateCrystalsText();

        // ���������� ����� ������� � ������
        UpdateRunnersText();
    }

    void UpdateCrystalsText()
    {
        // ��������, ��� UI-����� ��� ���������� ������
        if (crystalsText != null)
        {
            // ���������� ������ � ������ ����������
            crystalsText.text = "Crystals: " + drawingPanel.CrystalsCollected.ToString();
        }
    }

    void UpdateRunnersText()
    {
        // ��������, ��� UI-����� ��� ������� ������
        if (runnersText != null)
        {
            // ���������� ������ � ������ �������
            runnersText.text = "Runners: " + drawingPanel.RunnersCount.ToString();
        }
    }
}
