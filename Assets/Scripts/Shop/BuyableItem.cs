using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyableItem : MonoBehaviour {
    public int index;
    public int cost;
    public bool increasePrice = false;
    public Button button;

    public Text costText;

    void Update()
    {
        if (costText != null)
            costText.text = cost + " Points";
    }

    public enum ItemTypes
    {
        Weapon,
        Structure
    };

    public ItemTypes itemType;
}
