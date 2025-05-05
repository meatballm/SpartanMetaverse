using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{
    static IngameManager ingameManager;
    BaseUIManager uiManager;
    ScoreManager scoreManager;

    private int currentScore = 0;
    public static IngameManager Instance
    {
        get { return ingameManager; }
    }
    public BaseUIManager UIManager
    {
        get { return uiManager; }
    }

    private void Awake()
    {
        ingameManager = this;
        uiManager = FindObjectOfType<BaseUIManager>();
        scoreManager = ScoreManager.Instance;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        scoreManager.TrySetNewScore(currentScore);
        uiManager.SetRestart();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }

}