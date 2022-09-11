using UnityEngine;

namespace Ghosts
{
    public abstract class GhostState
    {
        protected readonly GhostStateManager Manager;
        public GhostState(GhostStateManager manager)
        {
            Manager = manager;
        }

        public abstract void EnterState();

        public abstract void UpdateState(GhostBrain brain);

        public abstract void EndState();
        

    }
    
}