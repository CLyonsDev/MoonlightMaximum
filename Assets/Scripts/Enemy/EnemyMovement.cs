using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    Transform target;

    Transform reactor;

    public float speed = 2.5f;
    public float stoppingDistance = 1f;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        reactor = GameObject.FindGameObjectWithTag("Reactor").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
	}
}
