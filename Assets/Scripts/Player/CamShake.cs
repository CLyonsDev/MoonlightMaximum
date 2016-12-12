using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour {

    Camera cam;

    Vector2 originalPos;
    Quaternion originalRot;

    float decay;
    public float intensity;

    public GameObject shakeFocusGO;
    GameObject dummy;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(intensity > 0)
        {
            /*if(dummy == null)
            {
                dummy = (GameObject)Instantiate(shakeFocusGO);
            }
            dummy.transform.position = originalPos + Random.insideUnitCircle * intensity;*/

            Vector3 newPos = originalPos + Random.insideUnitCircle * intensity;
            newPos.z = -11;

            cam.transform.position = newPos;
            intensity -= decay;

            //cam.GetComponent<CameraMovement>().player = dummy.transform;
        }
        else if(intensity <= 0)
        {
            //cam.GetComponent<CameraMovement>().player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    public void Shake(float intens, float dec)
    {
        originalPos = transform.position;
        originalRot = transform.rotation;

        decay = dec;
        intensity = intens;
    }
}
