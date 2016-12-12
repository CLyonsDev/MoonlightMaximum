using UnityEngine;
using System.Collections;

public class DestroyOnSoundComplete : MonoBehaviour {

    AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (!source.isPlaying)
            Destroy(gameObject);
	}
}
