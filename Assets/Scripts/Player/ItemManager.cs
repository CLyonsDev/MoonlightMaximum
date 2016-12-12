using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

    public List<GameObject> currentWeapons = new List<GameObject>();
    public List<GameObject> purchaseableWeapons = new List<GameObject>();

    private GameObject activeStructure;
    private GameObject ghost;
    public List<GameObject> currentStructures = new List<GameObject>();
    public List<GameObject> purchaseableStructures = new List<GameObject>();

    public GameObject cursor;

    public GameObject barricadeHealthBarGO;

    public CanvasRenderer[] buildRends;

    public GameObject Store;

    public bool storeActive = false;

    public bool isBuilding = false;

    int weaponIndex = 1;
    int structureIndex = 1;

    PlayerScore scoreScript;

	// Use this for initialization
	void Start () {
        scoreScript = GetComponent<PlayerScore>();
        ToggleBuildPopup();
        SwitchWeapon(weaponIndex);
	}
	
	// Update is called once per frame
	void Update () {
        ReadInputs();
        Build();
        ToggleStore();
    }

    private void ReadInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!isBuilding)
            {
                weaponIndex = 1;
                SwitchWeapon(weaponIndex);
            }
            else
            {
                structureIndex = 1;
                SwitchBuilding(structureIndex);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isBuilding)
            {
                weaponIndex = 2;
                SwitchWeapon(weaponIndex);
            }
            else
            {
                structureIndex = 2;
                SwitchBuilding(structureIndex);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isBuilding)
            {
                weaponIndex = 3;
                SwitchWeapon(weaponIndex);
            }
            else
            {
                structureIndex = 3;
                SwitchBuilding(structureIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwapIsBuilding();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(isBuilding)
            {
                if(structureIndex + 1 > currentStructures.Count)
                {
                    structureIndex = 1;
                }
                else
                {
                    structureIndex++;
                }

                SwitchBuilding(structureIndex);
            }
            else
            {
                if(weaponIndex + 1 > currentWeapons.Count)
                {
                    weaponIndex = 1;
                }
                else
                {
                    weaponIndex++;
                }

                SwitchWeapon(weaponIndex);
            }

        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (isBuilding)
            {
                if (structureIndex - 1 < 1)
                {
                    structureIndex = currentStructures.Count;
                }
                else
                {
                    structureIndex--;
                }
                SwitchBuilding(structureIndex);
            }
            else
            {
                if (weaponIndex - 1 < 1)
                {
                    weaponIndex = currentWeapons.Count;
                }
                else
                {
                    weaponIndex--;
                }
                SwitchWeapon(weaponIndex);
            }
        }
    }

    private void SwitchWeapon(int index)
    {
        //If we have a weapon at the index-1
        if(currentWeapons.Count >= index)
        {
            //Do our foreach loop
            foreach (GameObject go in currentWeapons)
            {
                if (go == currentWeapons[index - 1])
                    go.SetActive(true);
                else
                    go.SetActive(false);
            }
        }
    }

    private void SwitchBuilding(int index)
    {
        //If we have a weapon at the index-1
        if (currentStructures.Count >= index)
        {
            activeStructure = currentStructures[index - 1];

            if (ghost != null)
                GameObject.Destroy(ghost);
        }
    }

    private void SwapIsBuilding()
    {
        isBuilding = !isBuilding;
        //Debug.Log("Toggled Building to " + isBuilding);

        ToggleBuildPopup();

        if (isBuilding)
        {
            SwitchBuilding(1);
        }
        else
        {
            SwitchWeapon(1);
            if (ghost != null)
                GameObject.Destroy(ghost);
        }

        cursor.GetComponent<Crosshair>().UpdateCursor(isBuilding);
    }

    private void ToggleBuildPopup()
    {
        if (isBuilding)
        {
            buildRends[0].SetAlpha(0.5f);
            buildRends[1].SetAlpha(1);
        }
        else
        {
            buildRends[0].SetAlpha(0);
            buildRends[1].SetAlpha(0);
        }
    }

    private void Build()
    {
        if (!isBuilding)
            return;

        if(ghost == null && currentStructures.Count > 0)
        {
            GameObject newStructure = (GameObject)Instantiate(activeStructure, Vector3.zero, Quaternion.identity);
            newStructure.transform.localPosition = new Vector3(0, 0, 1);
            ghost = newStructure;
        }

        if(ghost != null)
        { 
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 1;
            pos = new Vector3(Mathf.Round(pos.x * 2f) * 0.5f, Mathf.Round(pos.y * 2f) * 0.5f, pos.z);
            ghost.transform.position = pos;
        }

        //When we click, spawn the structure.
        if (Input.GetMouseButtonDown(0) && ghost != null)
        {
            if(ghost.transform.tag == "Turret")
            {
                ghost.GetComponentInChildren<Tracking>().enabled = true;
                ghost.GetComponentInChildren<DAKADAKADAKADAKADAKADAKA>().enabled = true;
            }else if(ghost.transform.tag == "Barricade")
            {
                GameObject barricadeHealthBar = (GameObject)Instantiate(barricadeHealthBarGO, GameObject.Find("Worldspace Canvas").transform, true);
                barricadeHealthBar.transform.position = ghost.transform.position;
                ghost.GetComponent<BarricadeHealth>().healthBar = barricadeHealthBar.transform.FindChild("BarricadeHealthBear").gameObject.GetComponentInChildren<Image>();
            }
            
            ghost.GetComponent<Collider2D>().enabled = true;

            currentStructures.Remove(activeStructure);

            ghost = null;

            if (currentStructures.Count > 0)
                activeStructure = currentStructures[0];
            else
            {
                activeStructure = null;
                SwapIsBuilding();
            } 
        }
    }

    private void ToggleStore()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Store.SetActive(!Store.activeInHierarchy);

            if (Store.activeInHierarchy)
            {
                GameObject.Find("Crosshair").GetComponent<Crosshair>().SetCursorEnabled(true);
                TimescaleManager.SetPaused(true);

            }
            else
            {
                GameObject.Find("Crosshair").GetComponent<Crosshair>().SetCursorEnabled(false);
                TimescaleManager.SetPaused(false);
            }
        }
        storeActive = Store.activeInHierarchy;
    }

    public void SetStoreActive(bool isActive)
    {
        Store.SetActive(isActive);
        storeActive = isActive;

        if (Store.activeInHierarchy)
        {
            GameObject.Find("Crosshair").GetComponent<Crosshair>().SetCursorEnabled(true);
            TimescaleManager.SetPaused(true);

        }
        else
        {
            GameObject.Find("Crosshair").GetComponent<Crosshair>().SetCursorEnabled(false);
            TimescaleManager.SetPaused(false);
        }
    }

    public void PurchaseUpgrade(BuyableItem i)
    {
        if(scoreScript.points >= i.cost)
        {
            scoreScript.RemovePoints(i.cost);
            Debug.Log("Purchased upgrade for " + i.cost + " points!");

            if (i.itemType == BuyableItem.ItemTypes.Weapon)
            {
                currentWeapons.Add(purchaseableWeapons[i.index]);
                i.button.enabled = false;
                i.button.GetComponentInChildren<Text>().text = "PURCHASED";

            }
            else if (i.itemType == BuyableItem.ItemTypes.Structure)
            {
                currentStructures.Add(purchaseableStructures[i.index]);
                if(i.increasePrice == true)
                {
                    i.cost *= 3;
                }

                if(i.costText != null)
                {
                    i.costText.text = i.cost.ToString() + " Points";
                }
            }
        }
        else
        {
            Debug.Log("Not enough money! (You have " + scoreScript.points + " points but need " + i.cost + ")");
        }
    }
}
