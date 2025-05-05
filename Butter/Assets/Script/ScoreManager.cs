using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int[] HighScore { get; private set; }
    int GameCount=3;
    int currentGameIndex;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        HighScore = new int[GameCount];
        for(int i=0;i<3;i++)
            HighScore[i] = PlayerPrefs.GetInt("HighScore"+i, 0);
    }
    public void SetCurrentGameIndex(int idx)
    {
        if (0 <= idx && idx < HighScore.Length)
            currentGameIndex = idx;
        else
            Debug.LogWarning("잘못된 게임 인덱스: " + idx);
    }
    public void TrySetNewScore(int score)
    {
        if (score > HighScore[currentGameIndex])
        {
            HighScore[currentGameIndex] = score;
            PlayerPrefs.SetInt("HighScore"+currentGameIndex, HighScore[currentGameIndex]);
            PlayerPrefs.Save();
            Debug.Log("최고기록 갱신! : "+score);
        }
    }
}