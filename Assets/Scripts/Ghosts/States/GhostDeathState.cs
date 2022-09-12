using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostDeathState : GhostState
    {
        public override void EnterState()
        {
            Debug.Log($"{Manager.name} : Death State - START");
            //Graphical part
            Manager.SetBodyActive(false);
            Manager.SetEyesActive(true);
            
            //Set the target Tile
            TargetTile = Manager.brain.ChooseTargetTile(GState.Dead,
                Manager.GetPacmanPosition(),
                Manager.GetPacmanDirection(),
                GhostsManager.GetGhostPosition());
            Debug.Log($"The home tile is {TargetTile}");
            
            //Desactivate collision with PacMan.
            Manager.DisableGhostCollisionWithPacMan(true);
            
        }

        private Node _lastNode = null;
        public override void UpdateState()
        {
            //Check if we are at spawn
            if (((Vector2)Manager.Tf.position -  TargetTile).sqrMagnitude < 0.5f)
            {
                Manager.Tf.position = TargetTile;
                Manager.ChangeState(Manager.RespawnState);
                return;
            }
            
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
                    GState.Dead);
                Manager.Movement.ChangerDirection(dir);
            }
        }

        public override void EndState()
        {
            Manager.SetBodyActive(true);
            Manager.DisableGhostCollisionWithPacMan(false);
            Debug.Log($"--------- {Manager.name} : Death State - END");
        }

        public override void OnDrawGizmos()
        {
            Gizmos.DrawCube(new Vector3(TargetTile.x,TargetTile.y,0), 1f*Vector3.one);
        }

        public override void OnCollisionEnter()
        {
            //Do nothing.
        }

        public GhostDeathState(GhostStateManager manager) : base(manager)
        {
        }
    }
}