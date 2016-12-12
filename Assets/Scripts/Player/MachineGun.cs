using UnityEngine;
using System.Collections;

public class MachineGun : Gun {

    void Update () {
	    if(Input.GetMouseButton(0))
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if(currentAmmo == 0 && !reloading && Input.GetMouseButton(0))
        {
            Reload();
        }
	}
}
