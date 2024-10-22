using UnityEngine;

namespace EnClase
{
    // Este lo tiene el Prefab
    class NodeVisual : MonoBehaviour        
    {
        public GameObject GameObject { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
    }

    class Node
    {
        public NodeVisual NodeVisual { get; private set; }
        int data = 0;

        public Node()
        {
            //NodeVisual = GameManager.Instance.GetNodeInstance();
        }
    }

    class Tree
    {
        public Nodo root { get; private set; }

        public virtual void Insert(Node node)
        {

        }

        // public  -> 4 algoritmos de order

    }

    class TreeAVL : Tree
    {
        public override void Insert(Node node)
        {

        }

        // + 4 toraciones

    }
}