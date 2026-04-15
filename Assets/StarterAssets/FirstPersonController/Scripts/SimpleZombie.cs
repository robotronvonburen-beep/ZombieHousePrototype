using UnityEngine;

public class SimpleZombie : MonoBehaviour
{
    public Transform target;
    public bool followPlayerIfNoTarget = false;
    public string playerObjectName = "PlayerCapsule";

    public float moveSpeed = 1.5f;
    public float stopDistance = 0.05f;
    public float turnSpeed = 5f;

    [Header("Health Settings")]
    public int maxHealth = 30;
    public int currentHealth;

    [Header("Stun Settings")]
    public float stunDuration = 2f;
    public float pushForce = 2f;

    private Transform cachedPlayerTarget;
    private float stunTimer = 0f;
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead)
            return;

        if (stunTimer > 0f)
        {
            stunTimer -= Time.deltaTime;
            return;
        }

        if (followPlayerIfNoTarget && cachedPlayerTarget == null)
        {
            GameObject playerObject = GameObject.Find(playerObjectName);

            if (playerObject != null)
            {
                cachedPlayerTarget = playerObject.transform;
            }
        }

        Transform currentTarget = target;

        if (currentTarget == null && followPlayerIfNoTarget)
        {
            currentTarget = cachedPlayerTarget;
        }

        if (currentTarget == null)
            return;

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            Vector3 moveDirection = direction.normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z),
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void TakeHit(Vector3 hitDirection, int damageAmount)
    {
        if (isDead)
            return;

        stunTimer = stunDuration;
        transform.position += hitDirection.normalized * pushForce;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log(gameObject.name + " took damage. Health = " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }
}