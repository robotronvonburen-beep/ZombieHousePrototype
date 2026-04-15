using UnityEngine;

public class BarricadePlank : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Material healthyMaterial;
    public Material damagedMaterial;

    private Renderer plankRenderer;
    public bool IsBroken { get; private set; } = false;

    void Awake()
    {
        plankRenderer = GetComponentInChildren<Renderer>();

        if (plankRenderer == null)
        {
            Debug.LogWarning("No Renderer found on " + gameObject.name);
        }
    }

    void Start()
    {
        ResetPlank();
    }

    public void ResetPlank()
    {
        currentHealth = maxHealth;
        IsBroken = false;

        if (plankRenderer == null)
        {
            plankRenderer = GetComponentInChildren<Renderer>();
        }

        UpdateVisual();
    }

    public void TakeDamage(int damageAmount = 1)
    {
        if (IsBroken) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log(gameObject.name + " health = " + currentHealth);

        if (currentHealth <= 0)
        {
            BreakPlank();
            return;
        }

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (plankRenderer == null) return;

        if (currentHealth == maxHealth)
        {
            plankRenderer.material = healthyMaterial;
        }
        else
        {
            plankRenderer.material = damagedMaterial;
        }
    }

    void BreakPlank()
    {
        IsBroken = true;
        gameObject.SetActive(false);
    }
}