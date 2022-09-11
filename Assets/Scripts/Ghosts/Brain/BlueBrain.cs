using UnityEngine;

namespace Ghosts
{
    public class BlueBrain : GhostBrain
    {
        Vector2 posGoalInky = new Vector2(7.5f,-10.5f);
        public override Vector2 ChoosetargetTile(State state, Vector2 posPacman, Vector2 dirPacman, Vector2 posBlinky)
        {
            Vector2 target = Vector2.zero;

            if(state == State.Scatter)
            {
                target = posGoalInky;
            }
            else if(state == State.Chase)
            {
                Vector2 refPacman = (posPacman + 2*dirPacman);
                target = 2*refPacman - posBlinky;
            }
            else if(state == State.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}