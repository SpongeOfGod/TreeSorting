using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantityTxt;
    public TextMeshProUGUI NameTxt;
    public GameObject ShopManager;

    private ShopManagerScript shopManagerScript;
    [SerializeField] Button Buybutton;
    [SerializeField] Button Sellbutton;

    void Start()
    {
        shopManagerScript = ShopManager.GetComponent<ShopManagerScript>();
        UpdateButtonInfo();

        // Conectar el evento de clic del botón
        Buybutton.onClick.AddListener(OnBuybuttonClick);

        Sellbutton.onClick.AddListener(OnSellButtonClick);

    }

    public void UpdateButtonInfo()
    {
        if (shopManagerScript.shopItems[ItemID] != null)
        {
            Item item = shopManagerScript.shopItems[ItemID];
            PriceTxt.text = "Price: $" + item.Price.ToString("F2");
            QuantityTxt.text = item.Quantity.ToString();
            NameTxt.text = item.Name;
        }
        else
        {
            Debug.LogWarning("Invalid ItemID: " + ItemID);
        }
    }

    // Método que se llama al hacer clic en el botón
    private void OnBuybuttonClick()
    {
        // Llama al método Buy en ShopManagerScript y pasa el ItemID
        if (shopManagerScript != null)
        {
            shopManagerScript.Buy(ItemID);
        }
    }

    private void OnSellButtonClick()
    {
        if (shopManagerScript != null)
        {
            shopManagerScript.Sell(ItemID);
        }
    }
}

