using UnityEngine;

public class WindowBarricade : MonoBehaviour, IInteractable
{
    private bool isBarricaded = false;

    public void Interact()
    {
        if (isBarricaded)
        {
            Debug.Log("Already barricaded.");
            return;
        }

        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning("No PlayerInventory found.");
            return;
        }

        if (inventory.hasHammer && inventory.hasNails && inventory.hasPlanks)
        {
            Debug.Log("Barricading window!");

            isBarricaded = true;

            // Consume items (optional for now)
            inventory.hasNails = false;
            inventory.hasPlanks = false;

            // Visual change (simple for now)
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            Debug.Log("Missing items to barricade.");
        }
    }
}