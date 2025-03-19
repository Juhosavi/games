
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other
        )
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {
            playerController.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {
            if (playerController.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerController.Interactable = null;
            }
        }
    }
    public void Interact(PlayerController playerController)
    {
        playerController.DialogueUI.ShowDialogue(dialogueObject);
    }

}
