using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BeginGame : MonoBehaviour {

    public void BeginGameNowPlease()
    {
        SceneManager.LoadScene(1);
    }
}
