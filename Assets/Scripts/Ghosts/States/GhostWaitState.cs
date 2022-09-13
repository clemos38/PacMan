using UnityEngine;

namespace Ghosts
{
    public class GhostWaitState : GhostState
    {
        public GhostWaitState(GhostStateManager manager) : base(manager)
        {
        }

        public override void EnterState()
        {
            Manager.Tf.position = Manager.GetGhostSpawn();
            Manager.Movement.ChangerDirection(Vector2.zero); //No movement.
        }

        public override void UpdateState()
        {
            //Do nothing
        }

        public override void EndState()
        {
           //Do nothing
        }

        public override void OnDrawGizmos()
        {
            //Do nothing
        }
    }
}