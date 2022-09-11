using UnityEngine;

namespace Ghosts
{
    public class OrangeBrain : GhostBrain
    {
        Vector2 posGoalClyde = new Vector2(-10.5f,-10.5f);
        public override Vector2 ChooseTargetTile(GState state, Vector2 posPacman, Vector2 dirPacman, Vector2[] ghostsPos) //Clyde
        {
            var target = Vector2.zero;
            var posClyde = ghostsPos[1];

            if(state == GState.Scatter)
            {
                target = posGoalClyde;
            }
            else if(state == GState.Chase)
            {
                float distToPacman = Vector2.Distance(posClyde,posPacman);
                if(distToPacman>8)
                {
                    target = posPacman;
                }
                else
                {
                    target = posGoalClyde;
                }
            }
            else if(state == GState.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}