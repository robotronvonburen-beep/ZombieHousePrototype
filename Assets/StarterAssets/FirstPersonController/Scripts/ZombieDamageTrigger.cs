using UnityEngine;

public class ZombieDamageTrigger : MonoBehaviour
{
    public WindowPlankManager windowManager;
    public float damageInterval = 3f;

    private float timer = 0f;
    private SimpleZombie currentZombie;

    private void OnTriggerEnter(Collider other)
    {
        SimpleZombie zombie = other.GetComponentInParent<SimpleZombie>();

        if (zombie != null)
        {
            currentZombie = zombie;
            timer = 0f;
            Debug.Log("Zombie entered damage zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SimpleZombie zombie = other.GetComponentInParent<SimpleZombie>();

        if (zombie != null && zombie == currentZombie)
        {
            currentZombie = null;
            timer = 0f;
            Debug.Log("Zombie exited damage zone.");
        }
    }

    private void Update()
    {
        if (windowManager == null)
            return;

        if (currentZombie == null)
        {
            timer = 0f;
            return;
        }

        // If the zombie was destroyed while inside the trigger,
        // Unity may not call OnTriggerExit, so clear it here.
        if (!currentZombie.gameObject.activeInHierarchy)
        {
            currentZombie = null;
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= damageInterval)
        {
            windowManager.DamageRandomPlank();
            timer = 0f;
        }
    }
}