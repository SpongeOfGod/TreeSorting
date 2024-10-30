using System.Collections.Generic;
using UnityEngine;

public class SpawnABBTree : MonoBehaviour
{
    public TreeABB ABBTree = new TreeABB();
    [SerializeField] List<int> list = new List<int>();

    void Awake()
    {
        foreach (int i in list)
        {
            ABBTree.Insert(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            int randomNum = Random.Range(0, 99);
            ABBTree.Insert(randomNum);
        }
    }
}
