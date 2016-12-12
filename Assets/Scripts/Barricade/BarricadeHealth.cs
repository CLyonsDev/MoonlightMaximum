using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarricadeHealth : MonoBehaviour {

    float currentHealth;
    float maxHealth = 100;

    public Image healthBar;

    public GameObject explosionGO;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(healthBar != null)
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ((float)currentHealth / maxHealth), Time.deltaTime * 10);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(explosionGO, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (healthBar != null)
            Destroy(healthBar.transform.parent.gameObject);
    }
}
