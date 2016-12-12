using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    public GameObject[] spawnableEnemies;
    List<GameObject> enemySpawnPoints = new List<GameObject>();

    List<GameObject> enemiesAlive = new List<GameObject>();

    public Transform reactor;

    public GameObject player;
    bool isPlayerAlive = true;

    public GameObject explosionGO;

    int waveNum = 1;
    public int enemiesPerWave;

    bool waveOngoing = false;
    bool gameOver;
    public float downtimeDuration = 15f;
    float countdownTimer;

    public Text waveTimerText;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        countdownTimer = downtimeDuration;

        //waveTimerText = GameObject.Find("WaveTimer").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameOver)
            return;

        /*if(Input.GetKeyDown(KeyCode.M))
        {
            GameOver();
        }*/

        //Logic for time in-between waves
        if(!waveOngoing)
        {
            waveTimerText.text = "WAVE " + (waveNum) + " STARTS IN\n" + string.Format("{0:0}:{1:00}", Mathf.Floor(countdownTimer / 60), countdownTimer % 60);
            if (player.GetComponent<PlayerHealth>().isDead)
                return;

            countdownTimer -= Time.deltaTime;

            if(countdownTimer <= 0)
            {
                //The timer is done, and the wave should begin!
                countdownTimer = downtimeDuration;
                StartCoroutine(SpawnEnemies(enemiesPerWave * waveNum));
                waveOngoing = true;
            }
        }
        //Logic for when wave is in progress
        else
        {
            waveTimerText.text = "WAVE\n" + waveNum;
            enemiesAlive.Clear();
            enemiesAlive.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

            if(enemiesAlive.Count == 0)
            {
                //All enemies defeated, wave complete. Good job!
                waveOngoing = false;
                player.GetComponent<PlayerHealth>().Heal((player.GetComponent<PlayerHealth>().maxHealth / 3));
                waveNum++;
                return;
            }

            if(player.GetComponent<PlayerHealth>().isDead)
            {
                //Player has died.
                GameOver();
                return;
            }
        }
    }

    private void GameOver()
    {
        GameObject.Find("_Music").GetComponent<AudioSource>().Stop();
        StartCoroutine(BLOWEVERYTHINGUP(30));
        GetComponent<PopupManager>().SpawnPopupText("You have died. Restarting the game...");
        waveOngoing = false;
        gameOver = true;
    }

    private IEnumerator BLOWEVERYTHINGUP(int numExplosions)
    {
        float delay = 0.75f;
        for (int i = 0; i < numExplosions; i++)
        {
            yield return new WaitForSeconds(delay);
            delay -= 0.05f;
            if (delay < 0.1f)
                delay = 0.1f;

            Debug.Log(delay);

            Vector3 spawnPos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * 10;

            Camera.main.GetComponentInChildren<SoundList>().PlaySound(7);
            Instantiate(explosionGO, spawnPos, Quaternion.identity);

            Camera.main.GetComponent<CamShake>().Shake(0.5f, 0.04f);

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Random.Range(1, 4) == 1)
                    go.GetComponent<EnemyHealth>().TakeDamage(999);
            }

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Turret"))
            {
                if (Random.Range(1, 20) == 1)
                    go.transform.GetChild(0).gameObject.GetComponent<TurretDeath>().Explode();
            }
        }

        yield return new WaitForSeconds(1);
        Camera.main.GetComponentInChildren<SoundList>().PlaySound(9);
        GameObject finalExplosion = (GameObject)Instantiate(explosionGO, reactor.position, Quaternion.identity);
        finalExplosion.transform.localScale = new Vector3(64, 64, 64);
        finalExplosion.GetComponent<Animator>().speed = 0.75f;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<EnemyHealth>().TakeDamage(999);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Turret"))
        {
            go.transform.GetChild(0).gameObject.GetComponent<TurretDeath>().Explode();
        }

        Camera.main.GetComponent<CamShake>().Shake(1.5f, 0.025f);

        StartCoroutine(RestartGame(3));
    }

    private IEnumerator SpawnEnemies(int count)
    {
        enemySpawnPoints.Clear();
        enemySpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn"));

        //This is a GameJam Work-Around™
        //Do not attempt in real life
        GameObject spawnPoint1 = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
        GameObject newEnemy1 = (GameObject)Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length)], spawnPoint1.transform.position, spawnPoint1.transform.rotation);
        enemySpawnPoints.Clear();

        for (int i = 0; i < count - 1; i++)
        {
            if (enemySpawnPoints.Count <= 0)
            {
                enemySpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn"));
            }

            yield return new WaitForSeconds(0.2f);

            GameObject spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
            GameObject newEnemy = (GameObject)Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length - 1)], spawnPoint.transform.position, spawnPoint.transform.rotation);
            enemySpawnPoints.Remove(spawnPoint);
        }
    }

    private IEnumerator RestartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }
}
