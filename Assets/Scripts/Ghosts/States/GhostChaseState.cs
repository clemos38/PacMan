using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostChaseState : GhostState
    {
        private int _animationTriggerHash;
        public override void EnterState()
        {
            Debug.Log("Chase mode : START");
            //Graphical part
            Manager.SetAnimatorTrigger(_animationTriggerHash);
            Manager.SetSpriteColor(true);
            Manager.SetEyesActive(true);
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

        public GhostChaseState(GhostStateManager manager) : base(manager)
        {
            _animationTriggerHash = Animator.StringToHash("Normal");
        }

        public override void OnDrawGizmos()
        {
            //Draw the target
            Gizmos.DrawCube(new Vector3(TargetTile.x,TargetTile.y,0), 1f*Vector3.one);
            var pos = Manager.Tf.position;
            Gizmos.DrawLine(pos, pos + (Vector3)Manager.Movement.CurrentDir);
            // position -> TargetTile
            Gizmos.DrawLine(pos, TargetTile);
            
        }
    }
}