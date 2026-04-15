using UnityEngine;

public class HammerPickup : MonoBehaviour
{
    private GameObject hammerHeld;
    private PlayerToolState playerToolState;
    private PlayerInventory playerInventory;
    private bool alreadyPickedUp = false;

    private void Start()
    {
        GameObject player = GameObject.Find("PlayerCapsule");

        if (player != null)
        {
            playerToolState = player.GetComponent<PlayerToolState>();
            playerInventory = player.GetComponent<PlayerInventory>();

            Transform heldHammerTransform = player.transform.Find("PlayerCameraRoot/HeldItemAnchor/Hammer_Held");
            if (heldHammerTransform != null)
            {
                hammerHeld = heldHammerTransform.gameObject;
            }
        }
    }

    public void Pickup()
    {
        if (alreadyPickedUp)
            return;

        alreadyPickedUp = true;

        if (hammerHeld != null)
            hammerHeld.SetActive(true);

        if (playerToolState != null)
            playerToolState.hasHammer = true;

        if (playerInventory != null)
            playerInventory.hasHammer = true;

        gameObject.SetActive(false);

        Debug.Log("Hammer picked up through raycast interact.");
    }
}