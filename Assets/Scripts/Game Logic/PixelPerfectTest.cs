using UnityEngine;
using System.Collections;

public class PixelPerfectTest : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<Camera>().orthographicSize = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
