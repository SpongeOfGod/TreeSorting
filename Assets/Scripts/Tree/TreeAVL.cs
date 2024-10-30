using UnityEngine;

[System.Serializable]
    public class TreeAVL : TreeStructure
    {
        [SerializeField]  private AVLRotation AVLRotationManager;

        public override void Initialize()
        {
            AVLRotationManager = new AVLRotation(this);
        }
        public void Insert(int value)
        {
            root = InsertNode(root, value);
        }
        public override Nodo InsertNode(Nodo node, int value)
        {
            if (node == null)
            {
                return new Nodo(value);
            }
            if (value < node.dato)
            {
                depth++;
                node.izq = InsertNode(node.izq, value);
                node.izq.parent = node;
                node.izq.visualNode.ParentNode = node.dato.ToString();
                node.izq.visualNode.transform.SetParent(node.visualNode.transform, true);
                node.izq.positionX = -posX;
                node.izq.depth = CheckDepth(node) + 1;
                node.izq.SetVisualPosition(posY, offsetMultiplier);
                AVLRotationManager.CheckRotations(node.izq);
            }
            else if (value > node.dato)
            {
                depth++;
                node.der = InsertNode(node.der, value);
                node.der.parent = node;
                node.der.visualNode.ParentNode = node.dato.ToString();
                node.der.visualNode.transform.SetParent(node.visualNode.transform, true);
                node.der.positionX = posX;
                node.der.depth = CheckDepth(node) + 1;
                node.der.SetVisualPosition(posY, offsetMultiplier);
                AVLRotationManager.CheckRotations(node.der);
            }
            else
            {
                return node;
            }
            depth = 0;
            return node;
        }
        
    }
