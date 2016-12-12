using UnityEngine;
using System.Collections;

public class SoundList : MonoBehaviour {

    public AudioClip[] sounds;
    public GameObject soundGO;

    public void PlaySound(int index, float volume = 0.075f)
    {
        GameObject newSoundGO = (GameObject)Instantiate(soundGO, this.transform, false);
        newSoundGO.GetComponent<AudioSource>().clip = sounds[index];
        newSoundGO.GetComponent<AudioSource>().Play();
    }
}
