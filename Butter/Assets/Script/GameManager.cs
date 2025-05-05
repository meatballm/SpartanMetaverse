using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    public Vector2 playerlocation=Vector2.zero;
    public static GameManager Instance;
    public string[] gameName = {"Flappy","Jump","Stack","Survive" };
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
        player = GameObject.FindWithTag("Player");
        playerlocation = player.transform.position;
        SceneManager.LoadScene(gameName[i] + "Scene");
    }
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
