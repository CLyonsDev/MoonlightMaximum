using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {

    public float moveSpeed = 0.5f;
    public int damage = 0;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
        rb.MovePosition(transform.position + (transform.right * moveSpeed));
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
