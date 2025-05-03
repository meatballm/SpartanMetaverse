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
    public Button[] optionButtons;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(Sprite portrait, string npcName, string dialogue, string[] options, UnityAction[] callbacks)
    {
        dialoguePanel.SetActive(true);

        portraitImage.sprite = portrait;
        nameText.text = npcName;
        dialogueText.text = dialogue;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];

                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(callbacks[i]);
                optionButtons[i].onClick.AddListener(HideDialogue);
            }
            else { optionButtons[i].gameObject.SetActive(false); }
        }
    }
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
