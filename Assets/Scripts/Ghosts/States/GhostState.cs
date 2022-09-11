using UnityEngine;

namespace Ghosts
{
    public abstract class GhostState
    {
        protected readonly GhostStateManager Manager;
        protected readonly GhostsManager GhostsManager;
        protected Vector2 TargetTile;
        public GhostState(GhostStateManager manager)
        {
            Manager = manager;
            GhostsManager = GhostsManager.Singleton;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void EndState();
        

    }
    
}