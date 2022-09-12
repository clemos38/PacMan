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
        }

        private bool _reachHome = false;
        private float _timer = 0f;
        public override void UpdateState()
        {
            //Not working very well and cause many issues
            /***
            //Check if we are at spawn
            if (((Vector2)Manager.Tf.position -  TargetTile).sqrMagnitude < 4f)
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
            ***/

            if (_reachHome)
            {
                if(_timer <= 0)
                {
                    Manager.ChangeState(Manager.RespawnState);
                    return;
                }

                _timer -= Time.deltaTime;
                return;
            }

            Manager.Tf.position = Manager.GetGhostSpawn();
            _reachHome = true;
            _timer = 5f;
        }

        public override void EndState()
        {
            Manager.SetBodyActive(true);
            Manager.DisableGhostCollisionWithPacMan(false);
            Debug.Log($"--------- {Manager.name} : Death State - END");
        }

        public override void OnDrawGizmos()
        {
            //Nothing
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