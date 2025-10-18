using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeaponPrefab : MonoBehaviour
{
    public AreaWeapon weapon;
    private Vector3 targetSize;
    private float timer;
    public List<Enemy> enemiesInRange;
    private float counter;
    void Start()
    {
        weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;
        transform.localScale = Vector3.zero;
        timer = weapon.stats[weapon.weaponLevel].duration;
        AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        // betroo7 mn first param(local pos) la second param(target pos) w third param is over how much does it do that
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 3);
        //if shrinked el area effect only then destroy
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);
                AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponDespawn);
            }
        }
        // periodic dmg
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = weapon.stats[weapon.weaponLevel].attackrate;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                enemiesInRange[i].takeDamage(weapon.stats[weapon.weaponLevel].damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        enemiesInRange.Remove(collider.GetComponent<Enemy>());
    }
}
