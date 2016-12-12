using UnityEngine;
using System.Collections;

public class TurretDeath : MonoBehaviour {

    public GameObject standExplosionEffect, headExplosionEffect;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Explode();
        }
    }

	public void Explode()
    {
        GameObject s = (GameObject)Instantiate(standExplosionEffect);
        GameObject h = (GameObject)Instantiate(headExplosionEffect);

        s.transform.position = transform.parent.position;
        h.transform.position = this.transform.position;

        Destroy(transform.parent.gameObject);
    }
}
