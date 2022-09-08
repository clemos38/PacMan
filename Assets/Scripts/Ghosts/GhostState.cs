namespace Ghosts
{
    public abstract class GhostState
    {
        public abstract void EnterState();

        public abstract void UpdateState(GhostBrain brain);

        public abstract void EndState();

    }
}