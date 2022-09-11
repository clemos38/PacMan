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
            Manager.SetSpriteColor(Color.white);
            
        }

        public override void UpdateState(GhostBrain brain)
        {
            //Purely random movement

            //Check if we are on an intersection

            //if so, choose a random direction.
        }

        public override void EndState()
        {
            throw new System.NotImplementedException();
        }
        
    }
}