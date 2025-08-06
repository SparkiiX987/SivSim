public class PlanNode
{
    public PlanNode parent;
    public GoapAction action;
    public WorldState state;
    public float cost;

    public PlanNode(PlanNode _parent, GoapAction _action, WorldState _state, float _cost)
    {
        parent = _parent;
        action = _action;
        state = _state;
        cost = _cost;
    }

    public override bool Equals(object obj)
    {
        if (obj is PlanNode other)
           { return stateEquals(state, other.state); }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var kvp in state)
        {
            hash = hash * 23 + kvp.Key.GetHashCode();
            hash = hash * 23 + (kvp.Value?.GetHashCode() ?? 0);
        }
        return hash;
    }

    private bool stateEquals(WorldState a, WorldState b)
    {
        if (a.Count != b.Count)
            { return false; }

        foreach (var kvp in a)
        {
            if (!b.TryGetValue(kvp.Key, out var otherVal) || !Equals(kvp.Value, otherVal))
                { return false; }
        }
        return true;
    }
}
