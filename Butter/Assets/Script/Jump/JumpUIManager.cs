using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpUIManager : MonoBehaviour
{
    public static JumpUIManager Instance { get; private set; }

    public Button pauseButton;
    public GameObject pauseMenu; // Resume, Retry, Quit

    public TextMeshProUGUI heightText;
    public TextMeshProUGUI timeText;

    public Image chargeFill; // 게이지용 Image (Fill)

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText; // “Time: xx.xx s”

    void Awake()
    {
        Instance = this;
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);

        pauseButton.onClick.AddListener(() => {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        });

        // 버튼 콜백 연결
        pauseMenu.transform.Find("Resume").GetComponent<Button>()
            .onClick.AddListener(() => {
                Time.timeScale = 1;
                JumpGameManager.Instance.Resume();
            });
        pauseMenu.transform.Find("Retry").GetComponent<Button>()
            .onClick.AddListener(() => {
                Time.timeScale = 1;
                JumpGameManager.Instance.Retry();
            });
        pauseMenu.transform.Find("Quit").GetComponent<Button>()
            .onClick.AddListener(() => {
                Time.timeScale = 1;
                JumpGameManager.Instance.QuitGame();
            });
    }

    void Update()
    {
        if (Time.timeScale > 0 && !gameOverPanel.activeSelf)
        {
            float h = JumpGameManager.Instance.GetCurrentHeight();
            float t = JumpGameManager.Instance.GetElapsedTime();
            heightText.text = $"H: {h:F1}m";
            timeText.text = $"T: {t:F0}s";
        }
    }

    public void UpdateChargeUI(float normalized) =>
        chargeFill.fillAmount = normalized;

    public void ShowPauseButton() =>
        pauseButton.gameObject.SetActive(true);

    public void HidePauseMenu() =>
        pauseMenu.SetActive(false);

    public void ShowGameOver(float elapsed)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = $"Time: {elapsed:F2}s";

        gameOverPanel.transform.Find("Retry").GetComponent<Button>()
            .onClick.AddListener(() => {
                Time.timeScale = 1;
                JumpGameManager.Instance.Retry();
            });
        gameOverPanel.transform.Find("Quit").GetComponent<Button>()
            .onClick.AddListener(() => {
                Time.timeScale = 1;
                JumpGameManager.Instance.QuitGame();
            });
    }
}
