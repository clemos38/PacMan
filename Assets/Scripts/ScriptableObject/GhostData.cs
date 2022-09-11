using System;
using Ghosts;
using UnityEngine;

namespace CCLH
{
    [CreateAssetMenu(fileName = "Ghost", menuName = "App/GhostData", order = 0)]
    public class GhostData : ScriptableObject
    {
        public Color color;
        public GhostType type;


    }

    public enum GhostType
    {
        Red,
        Blue,
        Purple,
        Orange
    }
}