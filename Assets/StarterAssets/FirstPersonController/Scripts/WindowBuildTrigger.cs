using UnityEngine;

public class WindowBuildTrigger : MonoBehaviour
{
    public WindowBuildManager windowBuildManager;
    private bool playerInside = false;
    private PlayerInventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            playerInventory = FindFirstObjectByType<PlayerInventory>();
            Debug.Log("Player entered build trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player left build trigger.");
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (windowBuildManager != null)
            {
                windowBuildManager.InstallNextPlank(playerInventory);
            }
        }
    }
}