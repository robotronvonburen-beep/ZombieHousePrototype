using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        Hammer,
        Nails,
        Planks
    }

    public ItemType itemType;

    public void Interact()
    {
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning("No PlayerInventory found in scene.");
            return;
        }

        switch (itemType)
        {
            case ItemType.Hammer:
                inventory.hasHammer = true;
                Debug.Log("Picked up Hammer!");
                break;

            case ItemType.Nails:
                inventory.hasNails = true;
                Debug.Log("Picked up Nails!");
                break;

            case ItemType.Planks:
                inventory.hasPlanks = true;
                Debug.Log("Picked up Planks!");
                break;
        }

        Destroy(gameObject);
    }
}