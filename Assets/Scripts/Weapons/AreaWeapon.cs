using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon : Weapon
{
    [SerializeField] private GameObject Prefab;
    private float spawnCounter;

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].cooldown;
            Instantiate(Prefab, transform.position, transform.rotation, transform);
        }
    }
}
