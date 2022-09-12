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

        //private Vector3 _lastPosOnNode = Vector3.zero;
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
                //if ((pos - _lastPosOnNode).sqrMagnitude < 0.5f) return; //Avoid multiple call.
                //_lastPosOnNode = pos;
                var node = col.GetComponent<Node>();
                var dir = Manager.brain.ChooseDirection(node,
                    pos,
                                        TargetTile,
                           Manager.Movement.CurrentDir,
                                        GState.Scatter);
                Debug.Log($"We detect {node.name}.");
                Manager.Movement.ChangerDirection(dir);
            }
        }

        public override void EndState()
        {
           Debug.Log("End of Scatter mode.");
        }
        
    }
}