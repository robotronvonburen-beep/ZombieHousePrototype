using UnityEngine;

public class ZombieContactDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageInterval = 1f;

    private float damageTimer = 0f;
    private PlayerHealth playerHealthInRange;

    void Update()
    {
        if (playerHealthInRange == null)
            return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            playerHealthInRange.TakeDamage(damageAmount);
            damageTimer = 0f;
            Debug.Log("Zombie dealt contact damage.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerHealthInRange = other.GetComponent<PlayerHealth>();

        if (playerHealthInRange == null)
            playerHealthInRange = other.GetComponentInParent<PlayerHealth>();

        if (playerHealthInRange == null)
            playerHealthInRange = other.GetComponentInChildren<PlayerHealth>();

        if (playerHealthInRange != null)
        {
            damageTimer = 0f;
            Debug.Log("Zombie started damaging player.");
        }
        else
        {
            Debug.LogError("PlayerHealth not found on Player.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerHealthInRange = null;
        damageTimer = 0f;
        Debug.Log("Zombie stopped damaging player.");
    }
}