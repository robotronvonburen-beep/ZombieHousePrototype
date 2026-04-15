using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowPlankManager : MonoBehaviour
{
    public List<BarricadePlank> planks = new List<BarricadePlank>();

    [Header("Zombie Spawn")]
    public GameObject zombieInsidePrefab;
    public Transform zombieSpawnPoint;
    public GameObject outsideZombie;

    private bool zombieSpawned = false;

    void Start()
    {
        // IMPORTANT: check at start in case there are no planks at all
        CheckIfAllPlanksBroken();
    }

    public void DamageRandomPlank()
    {
        List<BarricadePlank> validPlanks = new List<BarricadePlank>();

        foreach (BarricadePlank plank in planks)
        {
            if (plank != null && plank.gameObject.activeSelf)
            {
                validPlanks.Add(plank);
            }
        }

        if (validPlanks.Count > 0)
        {
            int index = Random.Range(0, validPlanks.Count);
            validPlanks[index].TakeDamage(1);
        }

        CheckIfAllPlanksBroken();
    }

    void CheckIfAllPlanksBroken()
    {
        if (zombieSpawned) return;

        // CASE 1: No planks at all
        if (planks == null || planks.Count == 0)
        {
            SpawnZombieInside();
            return;
        }

        // CASE 2: All planks destroyed
        foreach (BarricadePlank plank in planks)
        {
            if (plank != null && plank.gameObject.activeSelf)
            {
                return; // still protected
            }
        }

        // No active planks left
        SpawnZombieInside();
    }

    void SpawnZombieInside()
    {
        zombieSpawned = true;

        if (outsideZombie != null)
        {
            Destroy(outsideZombie);
        }

        if (zombieInsidePrefab != null && zombieSpawnPoint != null)
        {
            Instantiate(zombieInsidePrefab, zombieSpawnPoint.position, zombieSpawnPoint.rotation);
        }
    }
}