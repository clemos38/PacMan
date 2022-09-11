using UnityEngine;
using CCLH;

namespace Ghosts
{
    public abstract class GhostBrain
    {
        public Vector2 posGhosthome = new Vector2(-2,1.2f);
        
        public abstract Vector2 ChooseTargetTile(GState state, Vector2 posPacman, Vector2 dirPacman, Vector2[] ghostsPos);

        /// <summary>
        /// This function return the direction at a certain node.
        /// </summary>
        /// <param name="node">The node to take the decision from</param>
        /// <param name="currentPosition">The position of the ghost</param>
        /// <param name="targetTile">The target tile the ghost must go to.</param>
        /// <param name="currentDirection">The current direction of the ghost.</param>
        /// <param name="state">The current state of the ghost</param>
        /// <returns>The direction to take as a Vector2.</returns>
        public Vector2 ChooseDirection(Node node, Vector2 currentPosition, Vector2 targetTile, Vector2 currentDirection, GState state)
        {
            //Init some angles
            var angleToTarget = (targetTile-currentPosition);
            var minAngle = 180.0f;
            var dirToGo = -currentDirection;

            if(state == GState.Frightened)
            {
                var num = Random.Range(0, node.AvailableDirections.Count);
                dirToGo = node.AvailableDirections[num];
            }

            else
            {
                //Direction check loop
                var angle = 0.0f; //Init
                foreach(var dirToTest in node.AvailableDirections) //We go through each possible direction (Vector2) in the node
                {
                    angle = Vector2.Angle(angleToTarget,dirToTest);
                    if(angle<minAngle && dirToTest!=(-currentDirection)) 
                    {
                        minAngle = angle;
                        dirToGo = dirToTest;
                    }
                }
            }

            return dirToGo;
        }

    }
    
    public enum GState
    {
        Scatter,
        Frightened,
        Chase,
        Dead
    }
}