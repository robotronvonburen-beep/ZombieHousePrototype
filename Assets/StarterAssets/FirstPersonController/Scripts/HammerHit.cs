using UnityEngine;

public class HammerHit : MonoBehaviour
{
    public Camera playerCamera;
    public float hitDistance = 2.5f;
    public float hitCooldown = 0.4f;
    public int damageAmount = 10;

    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (!gameObject.activeInHierarchy)
            return;

        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f)
        {
            TryHitZombie();
            cooldownTimer = hitCooldown;
        }
    }

    void TryHitZombie()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("HammerHit: No playerCamera assigned.");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitDistance))
        {
            SimpleZombie zombie = hit.collider.GetComponentInParent<SimpleZombie>();

            if (zombie != null)
            {
                Vector3 hitDirection = hit.collider.transform.position - playerCamera.transform.position;
                hitDirection.y = 0f;

                zombie.TakeHit(hitDirection, damageAmount);
                Debug.Log("Zombie hit with hammer.");
            }
        }
    }
}