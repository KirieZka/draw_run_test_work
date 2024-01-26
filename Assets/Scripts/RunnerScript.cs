using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public DrawingPanel drawingPanel;
    public EndGameUI endGameUI;

    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        endGameUI = GameObject.FindGameObjectWithTag("EndGame").GetComponent<EndGameUI>();
        drawingPanel = GameObject.FindGameObjectWithTag("DrawPanel").GetComponent<DrawingPanel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            CollectCrystal(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            HandleTrapCollision();
        }
        else if (other.CompareTag("HumanBonus"))
        {
            CollectBonusRunner(other.gameObject);
        }
        else if (other.CompareTag("Finish"))
        {
            endGameUI.ShowEndGameUI(true);
        }
    }

    private void CollectCrystal(GameObject crystal)
    {
        // Добавляем кристалл к счетчику и убираем его из сцены
        drawingPanel.CrystalsCollected++;
        Destroy(crystal);
    }

    private void CollectBonusRunner(GameObject bonusRunner)
    {
        // Добавляем бонусного бегуна к счетчику и убираем его из сцены
        drawingPanel.RunnersCount++;
        Destroy(bonusRunner);
    }

    private void HandleTrapCollision()
    {
        if (animator != null)
        {
            animator.SetBool("Dead", true);
        }
    }
}
