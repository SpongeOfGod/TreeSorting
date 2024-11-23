[System.Serializable]
public class TypeTwoVertices
{
    public VisualVertice StartVertice;
    public VisualVertice EndVertice;

    public TypeTwoVertices(VisualVertice startVertice, VisualVertice endVertice)
    {
        StartVertice = startVertice;
        EndVertice = endVertice;
    }
}