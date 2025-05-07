using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractableAnimationChanger : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator;
    public string interactTrigger = "Interact";
    public string exitTrigger = "ExitInteract";

    bool isInteracted = false;

    void Reset()
    {
        animator = GetComponent<Animator>();
    }

    public void SwapAnimation()
    {
        if (!isInteracted)
        {
            animator.SetTrigger(interactTrigger);
            isInteracted = true;
        }
        else
        {
            animator.SetTrigger(exitTrigger);
            isInteracted = false;
        }
    }
}
