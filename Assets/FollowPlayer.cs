using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    Transform player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
        transform.localPosition = player.position;
	}
}
