using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostChaseState : GhostState
    {
        public override void EnterState()
        {
            Debug.Log("Chase mode : START");
            //Maybe modify the speed of the ghost here.
        }

        private Node _lastNode = null;

        public override void UpdateState()
        {
            //Choose the target Tile
            TargetTile = Manager.brain.ChooseTargetTile(GState.Chase,
                Manager.GetPacmanPosition(),
                Manager.GetPacmanDirection(),
                GhostsManager.GetGhostPosition());
            //Check if we get on a node
            var pos = Manager.Tf.position;
            var col = Physics2D.OverlapBox(pos,
                0.5f * Vector2.one,
                0, Manager.nodeLayer);
            if (!(col is null))
            {
                var node = col.GetComponent<Node>();
                if (!(_lastNode is null) && _lastNode == node) return;
                _lastNode = node;
                var dir = Manager.brain.ChooseDirection(node,
                    pos,
                    TargetTile,
                    Manager.Movement.CurrentDir,
                    GState.Chase);
                Manager.Movement.ChangerDirection(dir);
            }
        }

        public override void EndState()
        {
            Debug.Log("-------- Chase mode : END");
        }

        public override void OnDrawGizmos()
        {
            //Do nothing
        }

        public GhostChaseState(GhostStateManager manager) : base(manager)
        {
        }
    }
}