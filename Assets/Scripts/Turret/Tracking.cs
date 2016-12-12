using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tracking : MonoBehaviour {

    public Transform target;
    public List<GameObject> enemies = new List<GameObject>();

    float thinkRate = 0.1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Think(thinkRate));
	}
	
	// Update is called once per frame
	void Update () {

        //Grab all enemies in the game
        enemies.Clear();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        if (target != null)
            TrackTarget();
            
    }

    private void GrabTargets()
    {
        //First, iterate through them to find the closest
        float minDist = 9999999;

        for (int i = 0; i < enemies.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, enemies[i].transform.position);

            if(dist < minDist)
            {
                //We can set our target to the one that is closest

                target = enemies[i].transform;
                minDist = dist;
            }
        }

        //Purge with extreme prejudice
    }

    private void TrackTarget()
    {
        //TODO: LASERRRRRRRRRRRRRRRRS

        Vector3 loc = target.position - transform.position;
        float angle = Mathf.Atan2(loc.y, loc.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.Rotate(-transform.forward * 90);
    }

    IEnumerator Think(float rate)
    {
        while(true)
        {
            yield return new WaitForSeconds(rate);
            if(enemies.Count > 0)
                GrabTargets();
        }
    }
}
