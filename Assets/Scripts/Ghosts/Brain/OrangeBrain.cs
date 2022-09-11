using UnityEngine;

namespace Ghosts
{
    public class OrangeBrain : GhostBrain
    {
        Vector2 posGoalClyde = new Vector2(-10.5f,-10.5f);
        public override Vector2 ChooseTargetTile(State state, Vector2 posPacman, Vector2 dirPacman, Vector2 posClyde)
        {
            Vector2 target = Vector2.zero;

            if(state == State.Scatter)
            {
                target = posGoalClyde;
            }
            else if(state == State.Chase)
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
            else if(state == State.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}