using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupManager : MonoBehaviour {

    public GameObject popup;

    public void SpawnPopupText(string text)
    {
        GameObject newPopup = (GameObject)Instantiate(popup, GameObject.Find("Canvas").transform, false);
        newPopup.GetComponentInChildren<Text>().text = text;
        newPopup.transform.localPosition = new Vector3(0, -32, 0);
    }
}
