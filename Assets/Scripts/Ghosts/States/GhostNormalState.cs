using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostNormalState : GhostState
    {
        private int _animationTriggerHash;
        public GhostNormalState(GhostStateManager manager) : base(manager)
        {
            _animationTriggerHash = Animator.StringToHash("Normal");
        }
        
        public override void EnterState()
        {
            //Make sure we have the appropriate sprite : modify it to the normal sprite
            Manager.SetAnimatorTrigger(_animationTriggerHash);
            Manager.SetSpriteColor(true);
            //TODO : Activer les yeux
        }

        private Node _lastNode = null;
        //TODO : 
        public override void UpdateState()
        {
            //Choose the target Tile
            TargetTile = Manager.brain.ChooseTargetTile(GState.Scatter,
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
                                        GState.Scatter);
                Manager.Movement.ChangerDirection(dir);
            }
        }

        public override void EndState()
        {
           Debug.Log("End of Scatter mode.");
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