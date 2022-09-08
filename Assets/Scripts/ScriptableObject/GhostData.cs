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

        private GhostBrain _brain;

        private void OnEnable()
        {
            switch (type)
            {
                case GhostType.Red:
                    _brain = new RedBrain();
                    break;
                case GhostType.Blue:
                    _brain = new BlueBrain();
                    break;
                case GhostType.Purple:
                    _brain = new PurpleBrain();
                    break;
                case GhostType.Orange:
                    _brain = new OrangeBrain();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public GhostBrain GetBrain() => _brain;
    }

    public enum GhostType
    {
        Red,
        Blue,
        Purple,
        Orange
    }
}