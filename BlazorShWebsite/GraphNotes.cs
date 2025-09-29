namespace BlazorShWebsite;

public class GraphNotes
{
    public Graph Graph { get; } = new();

    public void AddNode()
    {
        Graph.Nodes.Add(new Node());
        GenerateView();
    }

    private void GenerateView()
    {
        const int diameter = 100;
        const int radius = diameter / 2;
        var canvasSize = Convert.ToInt32(Math.Sqrt(Graph.Nodes.Count) * diameter * 2);
        (int x, int y) canvasMiddle = (canvasSize / 2, canvasSize / 2);
        var boundingBoxes = new HashSet<(int x, int y, int width, int height)>();

        var firstNode = true;
        
        foreach (var node in Graph.Nodes)
        {
            if (firstNode)
            {
                (int x, int y, int width, int height) boundingBox = (canvasMiddle.x - radius, canvasMiddle.x - radius, diameter, diameter);
                boundingBoxes.Add(boundingBox);
                firstNode = false;
                node.View.X = boundingBox.x;
                node.View.Y = boundingBox.y;
            }
            else
            {
                (int x, int y, int width, int height) boundingBox = (Random.Shared.Next(canvasSize), Random.Shared.Next(canvasSize), diameter, diameter);
                var spaceFound = false;
                foreach (var bb in boundingBoxes)
                {
                    if (boundingBox.x >= bb.x + bb.width || boundingBox.x + boundingBox.width <= bb.x)
                    {
                        if (boundingBox.y >= bb.y + bb.height || boundingBox.y + boundingBox.height <= bb.y)
                        {
                            spaceFound = true;
                            break;
                        }
                    }
                }

                if (!spaceFound)
                {
                    canvasSize += diameter;
                    var direction = Random.Shared.Next(1);
                    if (direction == 1)
                    {
                        boundingBox.x = canvasSize - diameter;
                        boundingBox.y = Random.Shared.Next(canvasSize);
                    }
                    else
                    {
                        boundingBox.y = canvasSize - diameter;
                        boundingBox.x = Random.Shared.Next(canvasSize);
                    }
                }

                node.View.X = boundingBox.x;
                node.View.Y = boundingBox.y;
                boundingBoxes.Add(boundingBox);
            }
        }
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
    public int X = 0;
    public int Y = 0;
    public string BackgroundColor = "red";
    public string OutlineColor = "black";
}

public class NodeView : GraphView
{
    public int Diameter = 100;
}

public class RelationshipView : GraphView
{
    
}