using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string[] gameName = {"Flappy","Jump","Stack","Survive" };
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void GoGameScene(int i)
    {
        SceneManager.LoadScene(gameName[i] + "Scene");
    }
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
