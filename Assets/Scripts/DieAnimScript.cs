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
        // Убираем бегуна из списка и уменьшаем счетчик бегунов
        drawingPanel.RunnersCount--;
        Destroy(gameObject);
    }
}
