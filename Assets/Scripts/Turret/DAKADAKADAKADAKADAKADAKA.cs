using UnityEngine;
using System.Collections;

public class DAKADAKADAKADAKADAKADAKA : MonoBehaviour {

    public GameObject projectile;

    public LayerMask lm;

    Transform target;

    Tracking t;

    float rateOfFire = 0.25f;
    int minDamage = 10;
    int maxDamage = 20;

    bool canShoot = true;

    RaycastHit2D hit;

	// Use this for initialization
	void Start () {
        t = GetComponent<Tracking>();
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null && t.target != null)
            target = t.target;

        if (target == null)
            return;

        Debug.DrawRay(transform.position, target.position - transform.position, Color.yellow, 0.1f);

        //Another way to do this is add a layermask, but we're being lazy
        hit = Physics2D.Raycast(transform.position, target.position - transform.position, Mathf.Infinity, lm);
        if (hit && canShoot)
        {
            //Debug.Log("WE 'IT A THING BUT WE DONT NO WOT IT IS");
            if (hit.transform.tag == "Enemy")
            {
                //Debug.Log("ITS A 'UMIE GET EM");
                //DAKAKAKDKDKAKDKADKAKDKAKDKKADKKA
                Daka();
            }
        }

        
	}

    private void Daka()
    {
        //Debug.LogWarning("DAKKKKKAAAA");
        GameObject proj = (GameObject)Instantiate(projectile, transform.FindChild("Barrel").position, transform.FindChild("Barrel").rotation);
        proj.GetComponent<ProjectileMovement>().damage = Random.Range(minDamage, maxDamage);
        proj.GetComponent<ProjectileMovement>().moveSpeed = 1;

        StartCoroutine(SoundDelay());
        StartCoroutine(ShootDelay(rateOfFire));
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.2f));
        Camera.main.GetComponentInChildren<SoundList>().PlaySound(6, 0.035f);
    }

    IEnumerator ShootDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
