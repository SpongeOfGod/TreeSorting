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
                node.izq.setParentNode(node);
                node.izq.positionX = -posX + offsetMultiplier * node.depth;
                node.izq.SetVisualPosition(posY, offsetMultiplier);
                depth = 0;
            } 
            else if (value > node.dato) 
            {
                depth++;
                node.der = InsertNode(node.der, value);
                node.der.visualNode.ParentNode = node.dato.ToString();
                node.der.visualNode.transform.SetParent(node.visualNode.transform, true);
                node.der.setParentNode(node);
                node.der.positionX = posX - offsetMultiplier * node.depth;
                node.der.SetVisualPosition(posY, offsetMultiplier);
                depth = 0;
            }
            else
            {
                return node;
            }

            return node;
        }
    }
