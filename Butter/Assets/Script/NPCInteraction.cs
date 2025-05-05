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
    public string npcDialogue = "�ȳ�! ������ �����ٱ�?";
    public string Btn1 = "���ӽ���";
    public string Btn2 = "�ְ���";
    public string Btn3 = "������";

    void Awake()
    {
        // "player" �±װ� ���� ������Ʈ�� ã�Ƽ� Transform �Ҵ�
        var go = GameObject.FindWithTag("Player");
        if (go != null)
            player = go.transform;
        else
            Debug.LogWarning("NPCInteraction: 'player' �±װ� ���� ������Ʈ�� ã�� �� �����ϴ�.");
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
        Debug.Log("NPC: �÷��̾ ���� �ȿ� ���Խ��ϴ�!");
        // ex) UI ǥ��
    }

    void OnExitRange()
    {
        Debug.Log("NPC: �÷��̾ ���� ������ �������ϴ�!");
        UIManager.instance.HideDialogue();
    }

    void Interact()
    {
        Debug.Log("NPC: ��ȣ�ۿ�!");
        StartDialogue();
    }
    void StartDialogue()
    {
        string[] options = { Btn1, Btn2, Btn3 };
        UnityAction[] callbacks = {
            () => GameManager.Instance.GoGameScene(GameId),
            () => UIManager.instance.UpdateDialogueText(
                GameManager.Instance.gameName[GameId]
                + "�� �ְ����� "
                + ScoreManager.Instance.ShowHighScore(GameId)
                + "�� �Դϴ�."
        ),() => Debug.Log("��ȭ ����")
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