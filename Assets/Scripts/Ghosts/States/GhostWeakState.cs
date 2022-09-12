using System.Collections;
using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostWeakState : GhostState
    {
        private int _animationTriggerHash;
        private int _animationStrengthenHash;
        public GhostWeakState(GhostStateManager manager) : base(manager)
        {
            _animationTriggerHash = Animator.StringToHash("Weak");
            _animationStrengthenHash = Animator.StringToHash("Strengthen");
        }
        public override void EnterState()
        {
            Debug.Log("Weak mode : START");
           //Go to weak animation
           Manager.SetAnimatorTrigger(_animationTriggerHash);
           Manager.SetSpriteColor(false);
           Manager.SetEyesActive(false);
        }

        private IEnumerator WeakDurationCoroutine()
        {
            //TODO : Make the timers set in the GameManager so as to be more changeable.
            yield return new WaitForSeconds(10);
            Manager.SetAnimatorTrigger(_animationStrengthenHash);
            yield return new WaitForSeconds(5);
            Manager.ChangeState(Manager.NormalState);
        }

        private Node _lastNode = null;
        public override void UpdateState()
        {
            //Choose the target Tile
            TargetTile = Manager.brain.ChooseTargetTile(GState.Frightened,
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
                    GState.Frightened);
                Manager.Movement.ChangerDirection(dir);
            }
        }

        public override void EndState()
        {
            Debug.Log("-------- Weak mode : END");
        }

        public override void OnDrawGizmos()
        {
            //Nothing
        }

        public override void OnCollisionEnter()
        {
            GameManager.Singleton.EatGhost(Manager);
            Manager.ChangeState(Manager.DeathState);
        }
    }
}