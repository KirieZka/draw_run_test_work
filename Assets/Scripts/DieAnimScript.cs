using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimScript : MonoBehaviour
{
    public DrawingPanel drawingPanel;

    private void Start()
    {
        drawingPanel = GameObject.FindGameObjectWithTag("DrawPanel").GetComponent<DrawingPanel>();
    }
    public void Die()
    {
        // ������� ������ �� ������ � ��������� ������� �������
        drawingPanel.RunnersCount--;
        Destroy(gameObject);
    }
}
