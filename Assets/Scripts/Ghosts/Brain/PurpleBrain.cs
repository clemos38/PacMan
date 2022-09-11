using UnityEngine;

namespace Ghosts
{
    public class PurpleBrain : GhostBrain
    {
        Vector2 posGoalPinky = new Vector2(-14.5f,13.5f);
        public override Vector2 ChoosetargetTile(State state, Vector2 posPacman, Vector2 dirPacman, Vector2 posUseless)
        {
            Vector2 target = Vector2.zero;

            if(state == State.Scatter)
            {
                target = posGoalPinky;
            }
            else if(state == State.Chase)
            {
                target = posPacman + 3*dirPacman;
            }
            else if(state == State.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}