using System.Collections;
using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    public float swingAngle = 60f;
    public float swingDownTime = 0.08f;
    public float swingUpTime = 0.12f;

    private bool isSwinging = false;
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging && gameObject.activeSelf)
        {
            StartCoroutine(SwingRoutine());
        }
    }

    private IEnumerator SwingRoutine()
    {
        isSwinging = true;

        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, swingAngle, 0f);

        float t = 0f;

        while (t < swingDownTime)
        {
            t += Time.deltaTime;
            float lerp = t / swingDownTime;
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, lerp);
            yield return null;
        }

        t = 0f;

        while (t < swingUpTime)
        {
            t += Time.deltaTime;
            float lerp = t / swingUpTime;
            transform.localRotation = Quaternion.Slerp(targetRotation, startRotation, lerp);
            yield return null;
        }

        transform.localRotation = startRotation;
        isSwinging = false;
    }
}