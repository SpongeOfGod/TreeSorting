using TMPro;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI valueText;

    public string Name = string.Empty;
    public int Value = 0;
    public int Type = 0;

    [SerializeField] private string typeName = string.Empty;
    public enum itemTypes 
    {
        Weapon, Armor, Consumable, Accessory
    }

    public void Awake() 
    {
        switch (Type) 
        {
            case 0:
                typeName = "Weapon";
                break;

            case 1:
                typeName = "Armor";
                break;

            case 2:
                typeName = "Consumable";
                break;

            case 3:
                typeName = "Accessory";
                break;
        }

        nameText.text = Name;
        itemTypeText.text = typeName.ToString();
        valueText.text = "$" + Value.ToString();
    }
}
