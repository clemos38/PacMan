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
            
            //Set a timer before letting them out. (Time set in the brain ?) 
        }

        public override void UpdateState()
        {
            //Check if the timer is gone
            
            //Leave the Spawn
            
            //If Spawn left, Change State
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