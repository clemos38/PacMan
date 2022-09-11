using UnityEngine;

namespace Ghosts
{
    public class PurpleBrain : GhostBrain
    {
        readonly Vector2 _posGoalPinky = new Vector2(-14.5f,13.5f);
        public override Vector2 ChooseTargetTile(GState state, Vector2 posPacman, Vector2 dirPacman, Vector2[] ghostsPos) //useless
        {
            var target = Vector2.zero;

            if(state == GState.Scatter)
            {
                target = _posGoalPinky;
            }
            else if(state == GState.Chase)
            {
                target = posPacman + 3*dirPacman;
            }
            else if(state == GState.Dead)
            {
                target = posGhosthome;
            }
            
            return target;
        }
    }
}