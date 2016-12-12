using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    Light ourLight;

    public float minDelay = 0.1f;
    public float maxDelay = 0.5f;

    public float minBrightness;
    public float maxBrightness;

	// Use this for initialization
	void Start () {
        ourLight = GetComponentInChildren<Light>();
        StartCoroutine(FlickerLight());
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator FlickerLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            ourLight.intensity = Random.Range(minBrightness, maxBrightness);
        }
    }
}
