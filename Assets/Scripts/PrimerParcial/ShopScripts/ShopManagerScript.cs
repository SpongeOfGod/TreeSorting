using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{
    public Dictionary<int, Item> shopItems = new Dictionary<int, Item>();
    public Dictionary<int, Item> playerInventory = new Dictionary<int, Item>();
    public List<GameObject> itemUI = new List<GameObject>();

    public Transform itemGrid;

    public float coins;
    public TextMeshProUGUI CoinsTXT;

    void Awake()
    {
        CoinsTXT.text = "Coins: " + coins.ToString();
        shopItems.Add(0, new Item("Sword", 10f, 20, "Weapon"));
        shopItems.Add(1, new Item("Boots", 20f, 1, "Armor"));
        shopItems.Add(2, new Item("Health Potion", 40f, 5, "Potion"));
        shopItems.Add(3, new Item("Magic Apple", 40f, 3, "Consumable"));
        shopItems.Add(4, new Item("Helmet", 50f, 1, "Armor"));
        shopItems.Add(5, new Item("Dagger", 60f, 3, "Weapon"));
        shopItems.Add(6, new Item("Defense Ring", 70f, 2, "Accessory"));
        shopItems.Add(7, new Item("Attack Ring", 80f, 1, "Accessory"));
    }

    public void SortByName()
    {
        Debug.Log("Sort by name");
        bool swapped = true;

        do
        {
            swapped = false;

            for (int i = 0; i < shopItems.Count - 1; i++)
            {
                shopItems.TryGetValue(i, out Item item1);
                shopItems.TryGetValue(i + 1, out Item item2);

                if (string.Compare(item1.Name, item2.Name) > 0)
                {
                    (shopItems[i], shopItems[i + 1]) = (shopItems[i + 1], shopItems[i]);

                    (itemUI[i].transform.position, itemUI[i + 1].transform.position) = (itemUI[i + 1].transform.position, itemUI[i].transform.position);
                    (itemUI[i], itemUI[i + 1]) = (itemUI[i + 1], itemUI[i]);

                    if (itemUI[i].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton1) && itemUI[i + 1].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton2))
                    {
                        (tempButton1.ItemID, tempButton2.ItemID) = (tempButton2.ItemID, tempButton1.ItemID);
                    }

                    swapped = true;

                }
            }
        } while (swapped);
    }

    public void SortByPrice()
    {
        Debug.Log("Sort by price");
        bool swapped = true;

        do
        {
            swapped = false;

            for (int i = 0; i < shopItems.Count - 1; i++)
            {
                shopItems.TryGetValue(i, out Item item1);
                shopItems.TryGetValue(i + 1, out Item item2);

                if (item1.Price > item2.Price)
                {
                    (shopItems[i], shopItems[i + 1]) = (shopItems[i + 1], shopItems[i]);

                    (itemUI[i].transform.position, itemUI[i + 1].transform.position) = (itemUI[i + 1].transform.position, itemUI[i].transform.position);
                    (itemUI[i], itemUI[i + 1]) = (itemUI[i + 1], itemUI[i]);

                    if (itemUI[i].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton1) && itemUI[i + 1].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton2))
                    {
                        (tempButton1.ItemID, tempButton2.ItemID) = (tempButton2.ItemID, tempButton1.ItemID);
                    }

                    swapped = true;

                }
            }
        } while (swapped);
    }

    public void SortByQuantity()
    {
        Debug.Log("Sort by quantity");
        bool swapped = true;

        do
        {
            swapped = false;

            for (int i = 0; i < shopItems.Count - 1; i++)
            {
                shopItems.TryGetValue(i, out Item item1);
                shopItems.TryGetValue(i + 1, out Item item2);

                if (item1.Quantity < item2.Quantity)
                {
                    (shopItems[i], shopItems[i + 1]) = (shopItems[i + 1], shopItems[i]);

                    (itemUI[i].transform.position, itemUI[i + 1].transform.position) = (itemUI[i + 1].transform.position, itemUI[i].transform.position);
                    (itemUI[i], itemUI[i + 1]) = (itemUI[i + 1], itemUI[i]);

                    if (itemUI[i].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton1) && itemUI[i + 1].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton2))
                    {
                        (tempButton1.ItemID, tempButton2.ItemID) = (tempButton2.ItemID, tempButton1.ItemID);
                    }

                    swapped = true;

                }
            }
        } while (swapped);
    }

    public void SortByCategory()
    {
        Debug.Log("Sort by category");
        bool swapped = true;

        do
        {
            swapped = false;

            for (int i = 0; i < shopItems.Count - 1; i++)
            {
                shopItems.TryGetValue(i, out Item item1);
                shopItems.TryGetValue(i + 1, out Item item2);

                if (string.Compare(item1.Category, item2.Category) > 0)
                {
                    (shopItems[i], shopItems[i + 1]) = (shopItems[i + 1], shopItems[i]);

                    (itemUI[i].transform.position, itemUI[i + 1].transform.position) = (itemUI[i + 1].transform.position, itemUI[i].transform.position);
                    (itemUI[i], itemUI[i + 1]) = (itemUI[i + 1], itemUI[i]);

                    if (itemUI[i].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton1) && itemUI[i + 1].TryGetComponent<ButtonInfo>(out ButtonInfo tempButton2))
                    {
                        (tempButton1.ItemID, tempButton2.ItemID) = (tempButton2.ItemID, tempButton1.ItemID);
                    }

                    swapped = true;

                }
            }
        } while (swapped);
    }



    public void Buy(int itemID)
    {
        Debug.Log("Buy method called for item ID: " + itemID);

        if (shopItems.ContainsKey(itemID))
        {
            Item itemToBuy = shopItems[itemID];

            if (coins >= itemToBuy.Price && itemToBuy.Quantity > 0)
            {
                coins -= itemToBuy.Price;
                itemToBuy.Quantity--;

                CoinsTXT.text = "Coins: " + coins.ToString();
                GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

                GameObject item = buttonRef.GetComponent<ItemAssociated>().itemAssociated;
                item.GetComponent<ButtonInfo>().QuantityTxt.text = itemToBuy.Quantity.ToString();

                if (playerInventory.ContainsKey(itemID))
                {
                    playerInventory[itemID].Quantity++;
                }

                else
                {
                    playerInventory.Add(itemID, itemToBuy.Clone());
                    playerInventory[itemID].Quantity = 1;
                }

                Debug.Log("Purchased " + itemToBuy.Name + ". Remaining stock: " + itemToBuy.Quantity);
            }
            else if (itemToBuy.Quantity <= 0)
            {
                Debug.Log("Out of stock");
            }
            else
            {
                Debug.Log("Not enough coins to buy this item. Please, get to work");
            }
        }
        else
        {
            Debug.Log("Item not found in the shop");
        }
    }

    public void Sell(int itemID)
    {
        if (playerInventory.ContainsKey(itemID))
        {
            Item itemToSell = playerInventory[itemID];

            if (itemToSell.Quantity > 0)
            {
                //Se vende por la mitad del precio original
                coins += itemToSell.Price / 2;

                itemToSell.Quantity--;

                //Si la cantidad del item llega a 0, se remueve del inventario
                if (itemToSell.Quantity == 0)
                {
                    playerInventory.Remove(itemID);
                }

                CoinsTXT.text = "Coins: " + coins.ToString();
                shopItems[itemID].Quantity++;

                GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

                GameObject item = buttonRef.GetComponent<ItemAssociated>().itemAssociated;
                item.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[itemID].Quantity.ToString();

                Debug.Log("Sold 1 " + itemToSell.Name + ". Player has " + itemToSell.Quantity + " remaining. ");
            }
            else
            {
                Debug.Log("You don't have any " + itemToSell.Name + " to sell.");
            }
        }

        else
        {
            Debug.Log("You don't own this item >:(");
        }
    }
}