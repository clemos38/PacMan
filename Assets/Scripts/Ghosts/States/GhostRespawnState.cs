using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostRespawnState : GhostState
    {
        private int _normalTriggerHash;
        public GhostRespawnState(GhostStateManager manager) : base(manager)
        {
            _normalTriggerHash = Animator.StringToHash("Normal");
        }
        public override void EnterState()
        {
            //Draw normal Ghost and comme back to Normal state
            Manager.SetAnimatorTrigger(_normalTriggerHash);
            Manager.SetBodyActive(true);
            Manager.SetEyesActive(true);
            Manager.SetSpriteColor(true);
            
            //Make sure he is at the correct position
            Manager.Tf.position = Manager.GetGhostSpawn();
            Manager.Movement.ChangerDirection(Vector2.zero); //No movement.

            //Set a timer before letting them out. (Time set in the brain ?)
            _waitToGoTimer = Manager.GetRespawnTimer();
        }

        private float _waitToGoTimer;

        public override void UpdateState()
        {
            if (_waitToGoTimer < 0)
            {
                Manager.Tf.position = new Vector3(-2f, 1.16f, 0f);
                Manager.Movement.ChangerDirection(Vector2.left);
                Manager.ChangeState(GameManager.Singleton.GhostPausedInScatter()
                    ? (GhostState) Manager.NormalState
                    : (GhostState) Manager.ChaseState);
            }
            else
            {
                _waitToGoTimer -= Time.deltaTime;
            }
            
        }

        public override void EndState()
        {
            //Nothing special
        }

        public override void OnDrawGizmos()
        {
            //Nothing to do here.
        }


        
    }
}