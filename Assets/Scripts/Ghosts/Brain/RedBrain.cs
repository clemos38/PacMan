using UnityEngine;

namespace Ghosts
{
    public class RedBrain : GhostBrain
    {
        Vector2 posGoalBlinky = new Vector2(10.5f,13.5f);
        public override Vector2 ChooseTargetTile(GState state, Vector2 posPacman, Vector2 dirPacman, Vector2[] ghostsPos) //useless
        {
            Vector2 target = Vector2.zero;

            if(state == GState.Scatter)
            {
                target = posGoalBlinky;
            }
            else if(state == GState.Chase)
            {
                target = posPacman;
            }
            else if(state == GState.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}