using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    //Our character's name is Max Damage

    float speed = 0.075f;

    Rigidbody2D rb;

    PlayerHealth ph;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        ph = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ph.isDead)
            return;

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(horiz * speed, vert * speed);

        rb.MovePosition(rb.position + moveDir);
	}
}
