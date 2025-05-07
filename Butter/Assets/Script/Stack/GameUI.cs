using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(StackUIManager uiManager)
    {
        base.Init(uiManager);

        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        comboText = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
    }
    public void SetUI(int score, int combo, int maxCombo)
    {
        scoreText.text = score.ToString()+" stack";
        comboText.text = combo.ToString()+" combo";
    }
}
