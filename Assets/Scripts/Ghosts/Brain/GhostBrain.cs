using UnityEngine;
using CCLH;

namespace Ghosts
{
    public abstract class GhostBrain
    {
        private Vector2 posGhosthome = new Vector2(-2,1.2f);
        public enum State
        {
            Scatter,
            Frightened,
            Chase
        }
        public abstract Vector2 ChoosetargetTile(State state);

        /// <summary>
        /// This function return the direction at a certain node.
        /// </summary>
        /// <param name="node">The node to take the decision from</param>
        /// <param name="currentPosition">The position of the ghost</param>
        /// <param name="targetTile">The target tile the ghost must go to.</param>
        /// <param name="currentDirection">The current direction of the ghost.</param>
        /// <returns>The direction to take as a Vector2.</returns>
        public Vector2 ChooseDirection(Node node, Vector2 currentPosition, Vector2 targetTile, Vector2 currentDirection)
        {
            //Init some angles
            var angleToTarget = (targetTile-currentPosition);
            var minAngle = 180.0f;
            var dirToGo = -currentDirection;
            
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
            
            return dirToGo;
        }

    }
}