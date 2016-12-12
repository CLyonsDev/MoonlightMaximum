using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public int damage = 10;
    public int barricadeDamage = 15;

    float attackDistance;
    public float attackDelay;

    bool canAttack = true;

    bool attackingPlayer = false;

    Transform target;

	// Use this for initialization
	void Start () {
        attackDistance = GetComponent<EnemyMovement>().stoppingDistance;
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (target.GetComponent<PlayerHealth>().isDead)
            return;

	    if(Vector2.Distance(transform.position, target.position) <= attackDistance && canAttack)
        {
            attackingPlayer = true;
            target.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(AttackDelay(attackDelay));
        }
        else
        {
            attackingPlayer = false;
        }
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.transform.tag == "Barricade" && !attackingPlayer && canAttack)
        {
            Debug.Log("Attacking Barricade");
            col.gameObject.GetComponent<BarricadeHealth>().TakeDamage(barricadeDamage);
            StartCoroutine(AttackDelay(attackDelay));
        }
    }

    IEnumerator AttackDelay(float delay)
    {
        canAttack = false;
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}
