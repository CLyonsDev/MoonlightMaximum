using UnityEngine;
using System.Collections;

public class DestroyAfterDelay : MonoBehaviour {

    public float delay;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfterTime(delay));
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
