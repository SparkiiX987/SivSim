using System.Collections.Generic;

public abstract class GoapAction
{
    public string actionName { get; set; }
    public float cost { get; set; }
    protected WorldState preconditions = new WorldState();
    protected WorldState effects = new WorldState();

    public IReadOnlyDictionary<string, object> Preconditions => preconditions;
    public IReadOnlyDictionary<string, object> Effects => effects;

    protected GoapAction(string _actionName, float _cost)
    {
        actionName = _actionName;
        cost = _cost;
    }


    public void AddPrecondition(string _key, bool _value) => preconditions[_key] = _value;

    public void AddEffect(string _key, bool _value) => effects[_key] = _value;

    public abstract bool ArePreconditionsMet(WorldState _currentWorldState);

    public abstract void Perform();

    public abstract bool IsDone();

    public abstract void Reset();

    public abstract bool CanExecute();
}
