using UnityEngine;
using System.Collections.Generic;

public class WindowBuildManager : MonoBehaviour
{
    public List<GameObject> plankObjects = new List<GameObject>();
    private int currentPlankIndex = 0;

    void Start()
    {
        HideAllPlanksAtStart();
    }

    void HideAllPlanksAtStart()
    {
        foreach (GameObject plank in plankObjects)
        {
            if (plank != null)
            {
                plank.SetActive(false);
            }
        }

        currentPlankIndex = 0;
    }

    public bool CanBuild(PlayerInventory inventory)
    {
        if (inventory == null) return false;
        return inventory.hasHammer && inventory.hasNails && inventory.hasPlanks;
    }

    public void InstallNextPlank(PlayerInventory inventory)
    {
        if (!CanBuild(inventory))
        {
            Debug.Log("Missing required items to install plank.");
            return;
        }

        if (currentPlankIndex >= plankObjects.Count)
        {
            Debug.Log("Window fully barricaded.");
            return;
        }

        GameObject plank = plankObjects[currentPlankIndex];

        if (plank != null)
        {
            plank.SetActive(true);

            BarricadePlank barricadePlank = plank.GetComponent<BarricadePlank>();
            if (barricadePlank != null)
            {
                barricadePlank.ResetPlank();
            }

            Debug.Log("Installed plank: " + plank.name);
        }

        currentPlankIndex++;
    }
}