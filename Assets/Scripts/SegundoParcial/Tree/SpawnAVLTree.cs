using System.Collections.Generic;
using UnityEngine;

public class SpawnAVLTree : MonoBehaviour
{
    public TreeAVL AVLTree = new TreeAVL();
    [SerializeField] List<int> list = new List<int>();

    void Awake()
    {
        AVLTree.Initialize();
        foreach (int i in list)
        {
            AVLTree.Insert(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            int randomNum = Random.Range(0, 99);
            AVLTree.Insert(randomNum);
        }
    }
}
