namespace BlazorShWebsite;

public class GraphNotes
{
    public Graph Graph { get; } = new();

    public void AddNode()
    {
        Graph.Nodes.Add(new Node());
    }

    private void GenerateView()
    {
        
    }
}

public class Graph
{
    public readonly HashSet<Node> Nodes = new();
}

public abstract class GraphObject
{
    public Guid id { get; init; } = Guid.NewGuid();
    public GraphView View { get; set; }
    private readonly Dictionary<string, string> _properties = new();

    public string GetPropertyOrEmptyString(string key)
    {
        return _properties.TryGetValue(key, out var value) ? value : string.Empty;
    }
}

public class Node : GraphObject
{
    public Node()
    {
        View = new NodeView();
    }
    
    public Dictionary<int, Relationship> Relationships { get; set; } = new();

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}

public class Relationship : GraphObject
{
    public Relationship()
    {
        View = new RelationshipView();
    }
    
    public Node Origin { get; set; } = new();
    public Node Destination { get; set; } = new();
}

public abstract class GraphView
{
    public int X;
    public int Y;
}

public class NodeView : GraphView
{
    
}

public class RelationshipView : GraphView
{
    
}