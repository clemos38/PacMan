using System.Collections;
using System.Threading.Tasks;
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

        private float _weakTimer;
        private float _strengthenTimer;
        public override void EnterState()
        {
            Debug.Log("Weak mode : START");
           //Go to weak animation
           Manager.SetAnimatorTrigger(_animationTriggerHash);
           Manager.SetSpriteColor(false);
           Manager.SetEyesActive(false);

           _weakTimer = 10f;
           _strengthenTimer = 5f;
        }
        

        private Node _lastNode = null;
        public override void UpdateState()
        {
            if (_weakTimer <= 0f)
            {
                if((int)_strengthenTimer == 5) Manager.SetAnimatorTrigger(_animationStrengthenHash);
                if (_strengthenTimer <= 0f)
                {
                    Manager.ChangeState(Manager.NormalState); //TODO : à modifier, dépend de l'état du cycle
                    return;
                }

                _strengthenTimer -= Time.deltaTime;
            }
            else
            {
                _weakTimer -= Time.deltaTime;
            }
            
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