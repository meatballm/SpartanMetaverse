using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpGameManager : MonoBehaviour
{
    public static JumpGameManager Instance { get; private set; }

    [Header("Finish Line Y")]
    public float finishY = 20f;

    float startY;
    float startTime;
    bool isGameOver = false;

    void Awake()
    {
        ScoreManager.Instance.currentGameIndex = 1;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        startY = FindObjectOfType<JumpPlayerController>().transform.position.y;
        startTime = Time.time;
        JumpUIManager.Instance.ShowPauseButton();
    }

    void Update()
    {
        if (isGameOver) return;

        float py = FindObjectOfType<JumpPlayerController>().transform.position.y;
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        float elapsed = Time.time - startTime;
        float height = GetCurrentHeight();
        ScoreManager.Instance.TrySetNewScore((int)(height));
        JumpUIManager.Instance.ShowGameOver(elapsed);
    }

    public void QuitGame()
    {
        GameManager.Instance.GoMainScene();
    }

    public void Retry()
    {
        GameManager.Instance.RestartGame();
    }

    public void Resume()
    {
        JumpUIManager.Instance.HidePauseMenu();
    }

    public float GetCurrentHeight() =>
        FindObjectOfType<JumpPlayerController>().transform.position.y - startY;

    public float GetElapsedTime() =>
        Time.time - startTime;
}
