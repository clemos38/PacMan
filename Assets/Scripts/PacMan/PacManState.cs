namespace PacMan
{
    public abstract class PacManState
    {
        public abstract void EnterState();
        
        public abstract void UpdateState();
        
        public abstract void EndState();
    }
}