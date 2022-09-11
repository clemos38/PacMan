namespace Ghosts
{
    public class GhostWeakState : GhostState
    {
        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(GhostBrain brain)
        {
            throw new System.NotImplementedException();
        }

        public override void EndState()
        {
            throw new System.NotImplementedException();
        }

        public GhostWeakState(GhostStateManager manager) : base(manager)
        {
        }
    }
}