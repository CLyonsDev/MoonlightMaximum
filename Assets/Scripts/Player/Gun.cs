using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour {

    public GameObject projectile;

    public List<Transform> barrels = new List<Transform>();

    public int minDamage;
    public int maxDamage;

    public float fireDelay;

    public int currentAmmo;
    public int maxAmmo;

    public float reloadDelay;
    public bool reloading = false;
    public bool canShoot = true;

    public int soundIndex;

    public float shakeIntensity = 0.2f;
    public float shakeDecay = 0.05f;

    void OnEnable()
    {
        canShoot = true;

        if (barrels.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                barrels.Add(transform.GetChild(i));
            }

            currentAmmo = maxAmmo;

        }
        GameObject.Find("BulletCounter").GetComponent<Text>().text = currentAmmo + " / " + maxAmmo;


    }

    void OnDisable()
    {
        StopAllCoroutines();
        reloading = false;
        canShoot = true;
    }

    public virtual void Shoot()
    {
        if (transform.parent.gameObject.GetComponent<PlayerHealth>().isDead || transform.parent.gameObject.GetComponent<ItemManager>().isBuilding || transform.parent.gameObject.GetComponent<ItemManager>().storeActive)
            return;


        if(currentAmmo > 0 && !reloading && canShoot)
        {
            Camera.main.GetComponent<CamShake>().Shake(shakeIntensity, shakeDecay);

            for (int i = 0; i < barrels.Count; i++)
            {
                GameObject proj = (GameObject)Instantiate(projectile, barrels[i].transform.position, barrels[i].transform.rotation);
                proj.GetComponent<ProjectileMovement>().damage = Random.Range(minDamage, maxDamage);
            }
            Camera.main.GetComponentInChildren<SoundList>().PlaySound(soundIndex);
            currentAmmo--;
            GameObject.Find("BulletCounter").GetComponent<Text>().text = currentAmmo + " / " + maxAmmo;
            StartCoroutine(ShootDelay(fireDelay));
        }     
    }

    public virtual void Reload()
    {
        if (transform.parent.gameObject.GetComponent<PlayerHealth>().isDead || currentAmmo == maxAmmo)
            return;

        StartCoroutine(ReloadWeapon(reloadDelay));
    }

    IEnumerator ShootDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    IEnumerator ReloadWeapon(float delay)
    {
        reloading = true;
        GameObject.Find("BulletCounter").GetComponent<Text>().text = "-RELOADING-";

        yield return new WaitForSeconds(delay);
        currentAmmo = maxAmmo;
        GameObject.Find("BulletCounter").GetComponent<Text>().text = currentAmmo + " / " + maxAmmo;
        reloading = false;
    }
}
