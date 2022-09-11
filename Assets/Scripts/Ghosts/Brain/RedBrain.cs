using UnityEngine;

namespace Ghosts
{
    public class RedBrain : GhostBrain
    {
        public override Vector2 ChoosetargetTile(State state, Vector2 posPacman)
        {
            Vector2 target = Vector2.zero;
            if(state == State.Scatter)
            {
                target[0]=10.5f;
                target[1]=13.5f;
            }
            else if(state == State.Chase)
            {
                target = posPacman;
            }
            else if(state == State.Frightened)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}