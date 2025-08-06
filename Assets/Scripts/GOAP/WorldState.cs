using System.Collections.Generic;

public class WorldState : Dictionary<string, object>
{
    public bool GetBool(string key)
    {
        return TryGetValue(key, out var value) && value is bool b ? b : false;
    }

    public int GetInt(string key)
    {
        return TryGetValue(key, out var value) && value is int i ? i : 0;
    }

    public float GetFloat(string key)
    {
        return TryGetValue(key, out var value) && value is float f ? f : 0f;
    }

    public T Get<T>(string key)
    {
        return TryGetValue(key, out var value) && value is T casted ? casted : default;
    }

    public void Set<T>(string key, T value)
    {
        this[key] = value;
    }

    public bool Satisfies(WorldState target)
    {
        foreach (var condition in target)
        {
            if (!HasCondition(condition.Key, condition.Value))
            {
                return false;
            }
        }
        return true;
    }

    public bool HasCondition(string key, object expectedValue)
    {
        return TryGetValue(key, out var value) && Equals(value, expectedValue);
    }

    public WorldState Clone()
    {
        WorldState copy = new WorldState();
        foreach (var kvp in this)
            { copy[kvp.Key] = kvp.Value; }
        return copy;
    }

    public override bool Equals(object _obj)
    {
        if (_obj is not WorldState other) { return false; }

        if (Count != other.Count) { return false; }

        foreach (var kvp in this)
        {
            if (!other.TryGetValue(kvp.Key, out var otherValue))
                { return false; }

            if (!Equals(kvp.Value, otherValue))
                { return false; }
        }

        return true;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var kvp in this)
        {
            int keyHash = kvp.Key.GetHashCode();
            int valueHash = kvp.Value != null ? kvp.Value.GetHashCode() : 0;
            hash = hash * 31 + keyHash ^ valueHash;
        }
        return hash;
    }

}
