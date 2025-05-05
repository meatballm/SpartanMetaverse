using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    public float interactionRadius = 1.5f;
    private KeyCode interactionKey = KeyCode.F;

    bool isPlayerInRange = false;
    Transform player;

    public int GameId;
    public Sprite npcPortrait;
    public string npcName = "NPC";
    public string npcDialogue = "안녕! 무엇을 도와줄까?";
    public string Btn1 = "게임시작";
    public string Btn2 = "최고기록";
    public string Btn3 = "다음에";

    void Awake()
    {
        // "player" 태그가 붙은 오브젝트를 찾아서 Transform 할당
        var go = GameObject.FindWithTag("Player");
        if (go != null)
            player = go.transform;
        else
            Debug.LogWarning("NPCInteraction: 'player' 태그가 붙은 오브젝트를 찾을 수 없습니다.");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRadius)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                OnEnterRange();
            }

            if (Input.GetKeyDown(interactionKey))
                Interact();
        }
        else if (isPlayerInRange)
        {
            isPlayerInRange = false;
            OnExitRange();
        }
    }

    void OnEnterRange()
    {
        Debug.Log("NPC: 플레이어가 범위 안에 들어왔습니다!");
        // ex) UI 표시
    }

    void OnExitRange()
    {
        Debug.Log("NPC: 플레이어가 범위 밖으로 나갔습니다!");
        UIManager.instance.HideDialogue();
    }

    void Interact()
    {
        Debug.Log("NPC: 상호작용!");
        StartDialogue();
    }
    void StartDialogue()
    {
        string[] options = { Btn1, Btn2, Btn3 };
        UnityAction[] callbacks = {
            () => GameManager.Instance.GoGameScene(GameId),
            () => UIManager.instance.UpdateDialogueText(
                GameManager.Instance.gameName[GameId]
                + "의 최고기록은 "
                + ScoreManager.Instance.ShowHighScore(GameId)
                + "점 입니다."
        ),() => Debug.Log("대화 종료")
        };

        bool[] closeOnSelect = { true, false, true };

        UIManager.instance.ShowDialogue(
            npcPortrait,
            npcName,
            npcDialogue,
            options,
            callbacks,
            closeOnSelect
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}