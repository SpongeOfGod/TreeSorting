[System.Serializable]
    public class AVLRotation
    {
        public TreeAVL AVLTree;
        Nodo p;
        Nodo q;
        public bool rotationOcurred;

        public AVLRotation(TreeAVL treeAVL)
        {
            AVLTree = treeAVL;
        }
        public void CheckRotations(Nodo nodo)
        {
            if (nodo == null) return;
            ChequearDerecha(nodo);
            ChequearIzquierda(nodo);
        }

        void ChequearIzquierda(Nodo nodo)
        {
            p = nodo;
            q = p.der;

            int balance = AVLTree.CheckDepth(nodo.der) - AVLTree.CheckDepth(nodo.izq);

            int balanceSubRight = 0;
            if (nodo.der != null)
            {
                balanceSubRight = AVLTree.CheckDepth(nodo.der.der) - AVLTree.CheckDepth(nodo.der.izq);
            }

            if (balance > 1 && balanceSubRight >= 0)
            {
                rotationOcurred = true;
                RotacionIzquierda(q, p);
            }
            else if (balance > 1 && balanceSubRight < 0)
            {
                rotationOcurred = true;
                RotacionDerecha(q.izq, q);
                RotacionIzquierda(q, p);
            }
        }

        void ChequearDerecha(Nodo nodo)
        {
            int balance = AVLTree.CheckDepth(nodo.der) - AVLTree.CheckDepth(nodo.izq);

            int balanceSubLeft = 0;

            if (nodo.izq != null)
            {
                balanceSubLeft = AVLTree.CheckDepth(nodo.izq.der) - AVLTree.CheckDepth(nodo.izq.izq);
            }

            p = nodo;
            q = p.izq;


            if (balance < -1 && balanceSubLeft <= 0)
            {
                rotationOcurred = true;
                RotacionDerecha(q, p);
            }
            else if (balance < -1 && balanceSubLeft > 0)
            {
                rotationOcurred = true;
                RotacionIzquierda(q.der, q);
                RotacionDerecha(q, p);
            }
        }

        public void EraseReferenceOfNode(Nodo parentNode, Nodo nodo)
        {
            if (parentNode.izq == nodo)
            {
                parentNode.izq = null;
            }

            if (parentNode.der == nodo)
            {
                parentNode.der = null;
            }
        }
        public void newPos(Nodo nodoGrab, float posX, float depth)
        {
            if (nodoGrab != null)
            {
                nodoGrab.visualNode.SetVisualPosition(AVLTree.posY, AVLTree.offsetMultiplier);
            }
        }

        public void RotacionDerecha(Nodo tempQ, Nodo tempP)
        {
            bool assigned = false;

            if (tempQ.der != null) // Si el nodo derecho de Q no es nulo, se lo asigna a P como nodo izquierdo
            {
                ReOrderNodeToLeft(tempQ, tempQ.der);
            }

            if (tempP.parent.der == tempP && !assigned)
            {
                ReOrderNodeToRight(tempP, tempQ);
                assigned = true;
            }

            if (tempP.parent.izq == tempP && !assigned)
            {
                ReOrderNodeToLeft(tempP, tempQ);
                assigned = true;
            }

            tempQ.der = tempP;
            tempQ.der.setParentNode(tempQ);
            tempQ.der.visualNode.gameObject.name = "Right";
            tempQ.der.positionX = AVLTree.posX - AVLTree.offsetMultiplier * tempQ.depth;
            tempQ.der.visualNode.transform.parent = tempQ.visualNode.transform;

            newPos(tempQ.der, AVLTree.posX, tempQ.der.depth);

            EraseReferenceOfNode(tempQ.der, tempQ);

            q = tempQ;
        }

        public void RotacionIzquierda(Nodo tempQ, Nodo tempP)
        {
            bool assigned = false;

            if (tempQ.izq != null)
            {
                ReOrderNodeToRight(tempQ, tempQ.izq);
            }

            if (tempP.parent.izq == tempP && !assigned)
            {
                ReOrderNodeToLeft(tempP, tempQ);
                assigned = true;
            }

            if (tempP.parent.der == tempP && !assigned)
            {
                ReOrderNodeToRight(tempP, tempQ);
                assigned = true;
            }

            tempQ.izq = tempP;
            tempQ.izq.setParentNode(tempQ);
            tempQ.izq.visualNode.gameObject.name = "Left";
            tempQ.izq.positionX = -AVLTree.posX + AVLTree.offsetMultiplier * tempQ.depth;
            tempQ.izq.visualNode.transform.parent = tempQ.visualNode.transform;
            newPos(tempQ.izq, -AVLTree.posX, tempQ.izq.depth);

            EraseReferenceOfNode(tempQ.izq, tempQ);

            q = tempQ;
        }

        public void ReOrderNodeToRight(Nodo parentNode, Nodo node)
        {
            parentNode.parent.der = node;
            parentNode.parent.der.setParentNode(parentNode);
            parentNode.parent.der.visualNode.gameObject.name = "Right";
            parentNode.parent.der.positionX = AVLTree.posX - AVLTree.offsetMultiplier * parentNode.parent.depth;
            parentNode.parent.der.visualNode.transform.parent = parentNode.parent.visualNode.transform;
            newPos(parentNode.parent.der, AVLTree.posX, parentNode.parent.depth);
        }

        public void ReOrderNodeToLeft(Nodo parentNode, Nodo node)
        {
            parentNode.parent.izq = node;
            parentNode.parent.izq.setParentNode(parentNode);
            parentNode.parent.izq.visualNode.gameObject.name = "Left";
            parentNode.parent.izq.positionX = -AVLTree.posX + AVLTree.offsetMultiplier * parentNode.parent.depth;
            parentNode.parent.izq.visualNode.transform.parent = parentNode.parent.visualNode.transform;
            newPos(parentNode.parent.izq, -AVLTree.posX, parentNode.parent.depth);
        }
    }
