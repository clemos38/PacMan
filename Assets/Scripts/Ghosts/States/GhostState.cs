using UnityEngine;

namespace Ghosts
{
    public abstract class GhostState
    {
        protected GhostStateManager Manager;
        public GhostState(GhostStateManager manager)
        {
            Manager = manager;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void EndState();
        

    }
    
}