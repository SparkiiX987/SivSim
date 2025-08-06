using System;
using System.Collections.Generic;
using UnityEngine;

public class GoapPlanner : MonoBehaviour
{
    public List<GoapAction> Plan(WorldState _initialState, WorldState _goal, List<GoapAction> _actions)
    {
        List<PlanNode> openSet = new List<PlanNode>();
        HashSet<PlanNode> closedSet = new HashSet<PlanNode>();
        Dictionary<PlanNode, float> gScore = new Dictionary<PlanNode, float>();
        Dictionary<PlanNode, float> fScore = new Dictionary<PlanNode, float>();

        PlanNode startNode = new PlanNode(null, null, _initialState.Clone(), 0);
        openSet.Add(startNode);
        gScore[startNode] = 0f;
        fScore[startNode] = Heuristic(_initialState, _goal);

        while (openSet.Count > 0)
        {
            PlanNode current = GetNodeWithLowestFScore(openSet, fScore);

            if (GoalReached(current.state, _goal))
                { return ReconstructPath(current); }

            openSet.Remove(current);
            closedSet.Add(current);
        }
        
        return null;
    }

    private List<GoapAction> ReconstructPath(PlanNode _current)
    {
        List<GoapAction> reconstructedPath = new List<GoapAction>();
        
        while(_current.action != null)
        {
            reconstructedPath.Add(_current.action);

            _current = _current.parent;
        }

        reconstructedPath.Reverse();
        return reconstructedPath;
    }

    private bool GoalReached(WorldState _currentState, WorldState _goal)
    {
        foreach (var kvp in _goal)
        {
            if (!_currentState.TryGetValue(kvp.Key, out var val) || !Equals(val, kvp.Value))
               { return false; }
        }
        return true;
    }


    private float Heuristic(WorldState _worldState, WorldState _goal)
    {
        throw new NotImplementedException();
    }

    public PlanNode GetNodeWithLowestFScore(List<PlanNode> _openSet, Dictionary<PlanNode, float> _fScore)
    {
        PlanNode currentNode = null;
        float lowestFScore = float.MaxValue;

        foreach (PlanNode node in _openSet)
        {
            float score = _fScore.ContainsKey(node) ? _fScore[node] : float.MaxValue;
            if (score < lowestFScore)
            {
                lowestFScore = score;
                currentNode = node;
            }
        }

        return currentNode;
    }


}
