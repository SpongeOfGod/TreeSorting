public abstract class GraphTDA<T> /* Conjunto TDA para el Grafo. */
{
    public abstract void InitializeGraph(GraphManager spawnGraph);
    public abstract bool AddVertice(T element);
    public abstract bool RemoveVertice(T element);
    public abstract bool VerticeExists(T element);
    public abstract TDA<T> Vertices();

    public abstract bool AddConnection(T verticeA, T verticeB);
    public abstract bool RemoveConnection(T verticeA, T verticeB);
    public abstract bool ConnectionExists(T verticeA, T verticeB);

    public abstract int ConnectionWeight(T verticeA, T verticeB);
}
