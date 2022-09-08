using UnityEngine;

// ReSharper disable once CheckNamespace
namespace CCLH
{
    [CreateAssetMenu(fileName = "NodeName", menuName = "NodeData", order = 0)]
    public class NodeData : ScriptableObject
    {
        public Sprite sprite;
        public NodeType type;

    }

    public enum NodeType
    {
        None,
        Pellet,
        PacGum,
        Cherry,
        Empty
    }
}