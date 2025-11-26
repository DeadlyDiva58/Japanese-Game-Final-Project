using EasyTalk.Controller;
using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public KeyCode activationKey = KeyCode.E; // Set your desired key in the Inspector

    private bool playerInTrigger = false;
    public GameObject dialogueSystem;
    DialogueController dialogueController;

    private void Start()
    {
        dialogueController = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueController>();

    }
    private void OnTriggerEnter(Collider other)
    {
        // Optional: Check for a specific tag or layer if you only want certain objects to interact
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            Debug.Log("Player entered trigger. Press " + activationKey.ToString() + " to interact.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerInTrigger && Input.GetKeyDown(activationKey))
        {

            dialogueController.PlayDialogue("Teacher1");
            Debug.Log("Button pressed while touching trigger!");
            // Add your interaction logic here (e.g., open a door, activate an item)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            Debug.Log("Player exited trigger.");
        }
    }
}
