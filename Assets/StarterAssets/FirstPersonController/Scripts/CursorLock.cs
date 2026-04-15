using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 1.5f;
    public float stopDistance = 1.2f;
    public float turnSpeed = 5f;

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
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

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}