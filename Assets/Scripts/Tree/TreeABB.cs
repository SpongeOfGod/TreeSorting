[System.Serializable]
    public class TreeABB : TreeStructure 
    {
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
                node.izq.visualNode.ParentNode = node.dato.ToString();
                node.izq.visualNode.transform.SetParent(node.visualNode.transform, true);
                node.izq.positionX = -posX;
                node.izq.depth = CheckDepth(node) + 1;
                node.izq.SetVisualPosition(posY, offsetMultiplier);
            } 
            else if (value > node.dato) 
            {
                depth++;
                node.der = InsertNode(node.der, value);
                node.der.visualNode.ParentNode = node.dato.ToString();
                node.der.visualNode.transform.SetParent(node.visualNode.transform, true);
                node.der.positionX = posX;
                node.der.depth = CheckDepth(node) + 1;
                node.der.SetVisualPosition(posY, offsetMultiplier);
            }
            else
            {
                return node;
            }

            depth = 0;
            return node;
        }
    }
