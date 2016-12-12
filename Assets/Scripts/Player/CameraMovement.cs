using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    float moveSpeed = 3f;

    public Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveToPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, moveToPos, Time.deltaTime * moveSpeed);
	}
}
