using UnityEngine;
using System.Collections;

public class Shotgun : Gun {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (currentAmmo == 0 && !reloading && Input.GetMouseButton(0))
        {
            Reload();
        }
    }
}
