using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int currentHealth, maxHealth;

    SpriteRenderer[] rends;

    public bool isDead;

    public Image healthBar;
    public Text healthText;

    public GameObject goreGO, bloodGO;

    float lerpSpeed = 5;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
        rends = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if(!isDead)
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ((float)currentHealth / maxHealth), Time.deltaTime * lerpSpeed);
        else
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, 0, Time.deltaTime * lerpSpeed * 5);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log("Took " + dmg + " damage! (" + currentHealth + " / " + maxHealth + ")");

        healthText.text = currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amt)
    {
        currentHealth += amt;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
    }

    void Die()
    {
        //TODO: Insert Game-Over Logic
        isDead = true;
        healthText.text = "-DEAD-";
        foreach (SpriteRenderer r in rends)
        {
            r.enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        Instantiate(bloodGO, transform.position, transform.rotation);
        Instantiate(goreGO, transform.position, transform.rotation);

        Camera.main.GetComponentInChildren<SoundList>().PlaySound(8, 0.1f);
    }
}
