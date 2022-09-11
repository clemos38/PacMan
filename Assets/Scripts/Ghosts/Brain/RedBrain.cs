using UnityEngine;

namespace Ghosts
{
    public class RedBrain : GhostBrain
    {
        Vector2 posGoalBlinky = new Vector2(10.5f,13.5f);
        public override Vector2 ChoosetargetTile(State state, Vector2 posPacman, Vector2 dirPacman, Vector2 posUseless)
        {
            Vector2 target = Vector2.zero;

            if(state == State.Scatter)
            {
                target = posGoalBlinky;
            }
            else if(state == State.Chase)
            {
                target = posPacman;
            }
            else if(state == State.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}