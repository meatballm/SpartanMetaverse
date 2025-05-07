using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {  get; private set; }

    public GameObject dialoguePanel;
    public Image portraitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI tutorialText;
    public Button[] optionButtons;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(
        Sprite portrait,
        string npcName,
        string dialogue,
        string Tutorial,
        string[] options,
        UnityAction[] callbacks,
        bool[] closeOnSelect = null)
    {
        dialoguePanel.SetActive(true);
        portraitImage.sprite = portrait;
        nameText.text = npcName;
        dialogueText.text = dialogue;
        tutorialText.text = Tutorial;
        // default: 모두 닫히도록
        if (closeOnSelect == null || closeOnSelect.Length != options.Length)
        {
            closeOnSelect = new bool[options.Length];
            for (int k = 0; k < options.Length; k++)
                closeOnSelect[k] = true;
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length)
            {
                var btn = optionButtons[i];
                btn.gameObject.SetActive(true);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = options[i];
                btn.onClick.RemoveAllListeners();

                // 1) 원래 콜백
                btn.onClick.AddListener(callbacks[i]);
                // 2) closeOnSelect[i]가 true일 때만 대화창 닫기
                if (closeOnSelect[i])
                    btn.onClick.AddListener(HideDialogue);
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void UpdateDialogueText(string newText)
    {
        dialogueText.text = newText;
    }
}
