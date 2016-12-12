using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 100;

    public GameObject gibGO;
    public GameObject gibParticleGO;

    public AudioClip[] deathSounds;
    public GameObject soundGO;

    public float pitch;

    public int scoreFromDefeated = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject.Find("Player").GetComponent<PlayerScore>().AddPoints(scoreFromDefeated);

        GameObject gore = (GameObject)Instantiate(gibGO, transform.position, transform.rotation);
        gore.transform.localScale = transform.localScale;
        gore.GetComponent<Animator>().speed = (Random.Range(1, 2f));

        Instantiate(gibParticleGO, transform.position, transform.rotation);

        AudioClip deathSound = deathSounds[Random.Range(0, deathSounds.Length)];

        GameObject newSoundGO = (GameObject)Instantiate(soundGO, transform.position, Quaternion.identity);

        newSoundGO.GetComponent<AudioSource>().clip = deathSound;
        newSoundGO.GetComponent<AudioSource>().volume = 0.075f;
        newSoundGO.GetComponent<AudioSource>().Play();
        newSoundGO.GetComponent<AudioSource>().pitch = pitch;

        Destroy(gameObject);
    }
}
