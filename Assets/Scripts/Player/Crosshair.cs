using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Crosshair : MonoBehaviour {

    //Boo
    bool cursorVisible = true;

    public GameObject shopWindow;

    public Sprite[] sprites;


	// Use this for initialization
	void Start () {
        SetCursorEnabled(false);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -9;
        transform.position = pos;

        if(Input.GetKeyDown(KeyCode.Escape) && !cursorVisible)
        {
            SetCursorEnabled(true);
        }else if(Input.GetMouseButtonDown(0) && cursorVisible && !shopWindow.activeInHierarchy)
        {
            SetCursorEnabled(false);
        }
    }

    public void SetCursorEnabled(bool vis)
    {
        cursorVisible = vis;

        if (!cursorVisible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UpdateCursor(bool building)
    {
        if (building)
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        else
            GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
