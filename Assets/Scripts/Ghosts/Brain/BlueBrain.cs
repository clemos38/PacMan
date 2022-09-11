using UnityEngine;

namespace Ghosts
{
    public class BlueBrain : GhostBrain
    {
        Vector2 posGoalInky = new Vector2(7.5f,-10.5f);
        public override Vector2 ChooseTargetTile(GState state, Vector2 posPacman, Vector2 dirPacman, Vector2[] ghostsPos) //blinky
        {
            var target = Vector2.zero;
            var posBlinky = ghostsPos[0];

            if(state == GState.Scatter)
            {
                target = posGoalInky;
            }
            else if(state == GState.Chase)
            {
                Vector2 refPacman = (posPacman + (2/3)*dirPacman);
                target = 2*refPacman - posBlinky;
            }
            else if(state == GState.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}