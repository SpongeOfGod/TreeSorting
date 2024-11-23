using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();
    [SerializeField] List<GameObject> itemsInParent = new List<GameObject>();
    [SerializeField] private Transform parentGrid;
    [SerializeField] private int startItems = 10;
    [SerializeField] Button addItemButton;
    [SerializeField] Button eraseItemButton;
    [SerializeField] Button SortValue;
    [SerializeField] Button SortName;
    [SerializeField] Button SortType;
    [SerializeField] int maxNumberOfItems = 19;

    [SerializeField] GameObject arrow;
    private int timesSorted = 0;
    private float moveDuration = 0.2f;

    private void Awake()
    {
        addItemButton.onClick.AddListener(CreateItem);
        eraseItemButton.onClick.AddListener(EraseItem);
        SortValue.onClick.AddListener(SortByValue);
        SortName.onClick.AddListener(SortByName);
        SortType.onClick.AddListener(SortByType);
    }

    private void Start()
    {
        for (int i = 0; i < startItems; i++)
        {
            CreateItem();
        }
        SortByValue();
    }

    public void SortByValue()
    {
        timesSorted = 0;

        for (int i = 0 + timesSorted; i < itemsInParent.Count; i++)
        {
            int index = i;

            for (int j = i; j < itemsInParent.Count; j++)
            {
                if (itemsInParent[index].GetComponent<ItemInventory>().Value < itemsInParent[j].GetComponent<ItemInventory>().Value)
                {
                    index = j;
                }
            }

            GameObject currentItemGameObject = itemsInParent[i].gameObject;
            GameObject nextItemGameObject = itemsInParent[index].gameObject;

            ItemInventory CurrentItem = currentItemGameObject.GetComponent<ItemInventory>();
            ItemInventory NextItem = nextItemGameObject.GetComponent<ItemInventory>();

            if (CurrentItem.Value < NextItem.Value)
            {
                (itemsInParent[i], itemsInParent[index]) = (itemsInParent[index], itemsInParent[i]);
                SetSiblingIndex(index, i);
                timesSorted++;
            }
        }

        MoveArrow("Value");
    }

    public void SortByName()
    {
        timesSorted = 0;

        while (timesSorted < itemsInParent.Count)
        {
            for (int i = 0; i < itemsInParent.Count - timesSorted; i++)
            {
                int index = i;

                if (i == itemsInParent.Count - timesSorted - 1)
                {
                    timesSorted++;
                }

                for (int j = 0; j < itemsInParent.Count - timesSorted; j++)
                {
                    GameObject currentItemGameObject = itemsInParent[index].gameObject;
                    GameObject nextItemGameObject = itemsInParent[j].gameObject;

                    ItemInventory CurrentItem = currentItemGameObject.GetComponent<ItemInventory>();
                    ItemInventory NextItem = nextItemGameObject.GetComponent<ItemInventory>();

                    if (CurrentItem.Name[0] < NextItem.Name[0])
                    {
                        (itemsInParent[index], itemsInParent[j]) = (itemsInParent[j], itemsInParent[index]);
                        SetSiblingIndex(index, j);
                    }
                }
            }
        }

        MoveArrow("Name");
    }

    private void SortByType()
    {
        QuickSort(0, itemsInParent.Count - 1);
        MoveArrow("Type");
    }

    private void QuickSort(int indexA, int indexB) // Agarra los index, y ve si ya se llego a la mitad del array/lista.
    {
        if (indexA < indexB)
        {
            int indexPartition = Dividing(indexA, indexB); // Empieza a agarrar y comparar cada numero en el index.

            QuickSort(indexA, indexPartition - 1); // Vuelve a ordenar el index, pero esta vez, desde el inicio hasta la mitad.
            QuickSort(indexPartition + 1, indexB); // Ordena desde la mitad, hasta el final.
        }
    }

    private int Dividing(int indexLow, int indexHigh)
    {
        int pivot = indexHigh;

        int lowestPointer = indexLow - 1; // Guarda el indice del primer objeto de la lista

        for (int i = indexLow; i <= indexHigh; i++)
        {
            var tempItem1 = itemsInParent[i];
            if (itemsInParent[i].GetComponent<ItemInventory>().Type < itemsInParent[pivot].GetComponent<ItemInventory>().Type)
            {
                lowestPointer = lowestPointer + 1; // Si encuentra uno de valor más bajo que el pivot, lo intercambia con el primer numero de la lista

                (itemsInParent[lowestPointer], itemsInParent[i]) = (itemsInParent[i], itemsInParent[lowestPointer]);
                SetSiblingIndex(lowestPointer, i);                
                
            }
        }
        (itemsInParent[lowestPointer + 1], itemsInParent[indexHigh]) = (itemsInParent[indexHigh], itemsInParent[lowestPointer + 1]);
        SetSiblingIndex(lowestPointer + 1, indexHigh); // Intercambia el pivot con el siguiente al numero del más bajo.
        return lowestPointer + 1;
    }

    private void SetSiblingIndex(int firstIndex, int secondIndex)
    {
        itemsInParent[firstIndex].transform.SetSiblingIndex(firstIndex);
        itemsInParent[secondIndex].transform.SetSiblingIndex(secondIndex);
    }

    private void CreateItem()
    {
        if (parentGrid.transform.childCount < maxNumberOfItems)
        {
            int itemIndex = Random.Range(0, items.Count);

            GameObject item = Instantiate(items[itemIndex], parentGrid);
            itemsInParent.Add(item);
        }
    }

    private void EraseItem()
    {
        if (parentGrid.transform.childCount > 0)
        {
            int itemIndex = Random.Range(0, parentGrid.transform.childCount);

            GameObject itemToKill = parentGrid.transform.GetChild(itemIndex).gameObject;

            itemsInParent.Remove(itemToKill);
            Destroy(itemToKill);
        }
    }

    private void MoveArrow(string sortType)
    {
        Vector3 targetPosition = Vector3.zero;

        switch (sortType)
        {
            case "Value":
                targetPosition = new Vector3(-0.41f, -0.08f, 0);
                break;
            case "Name":
                targetPosition = new Vector3(0.029f, -0.08f, 0);
                break;
            case "Type":
                targetPosition = new Vector3(0.468f, -0.08f, 0);
                break;
        }

        StartCoroutine(LerpArrow(arrow.transform, targetPosition, moveDuration));
    }

    private IEnumerator LerpArrow(Transform obj, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = obj.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = targetPosition;
    }
}
