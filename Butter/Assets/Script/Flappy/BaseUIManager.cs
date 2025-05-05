using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject restartText;

    public void Start()
    {
        if (restartText == null)
        {
            Debug.LogError("restart text is null");
        }

        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }

        restartText.gameObject.SetActive(false);
        UpdateScore(0);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}