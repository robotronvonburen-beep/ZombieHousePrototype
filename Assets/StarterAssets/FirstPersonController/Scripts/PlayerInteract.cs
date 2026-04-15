using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;
    public LayerMask interactLayers = ~0;

    private void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("PlayerInteract on '" + gameObject.name + "': No camera assigned.");
        }
    }

    private void Update()
    {
        if (playerCamera == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayers))
            {
                Debug.Log("Ray hit: " + hit.collider.name);

                HammerPickup hammerPickup = hit.collider.GetComponentInParent<HammerPickup>();
                if (hammerPickup != null)
                {
                    hammerPickup.Pickup();
                    return;
                }

                hit.collider.gameObject.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                hit.collider.transform.root.gameObject.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}