using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<PlayerHealth>().isDead)
            return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(pos);
        Quaternion rot = Quaternion.LookRotation(transform.position - pos, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        transform.Rotate(transform.forward * 90);
	}
}
