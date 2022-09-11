namespace Ghosts
{
    public class GhostDeathState : GhostState
    {
        public override void EnterState()
        {
            //Play the death animation
            //Change the sprite
            //Wait for the death animation to finish.
        }

        public override void UpdateState(GhostBrain brain)
        {
            //Computing direction to return home.
        }

        public override void EndState()
        {
            throw new System.NotImplementedException();
        }


        public GhostDeathState(GhostStateManager manager) : base(manager)
        {
        }
    }
}