using UnityEngine;
using System.Collections;

public class ToggleMusic : MonoBehaviour {

    private bool musicEnabled = true;

    float vol;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        vol = source.volume;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            ToggleMusicForMePlease();
        }
    }

	public void ToggleMusicForMePlease()
    {
        musicEnabled = !musicEnabled;

        if (musicEnabled)
            source.volume = vol;
        else
            source.volume = 0;
    }
}
