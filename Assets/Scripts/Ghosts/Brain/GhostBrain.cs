using UnityEngine;
using CCLH;

namespace Ghosts
{
    public abstract class GhostBrain
    {
        public abstract Vector2 ChoosetargetTile();

        public Vector2 ChooseDirection(Node node, Vector2 CurrentPosition, Vector2 targetTile, Vector2 CurrentDirection)
        {
            Vector2 AngleToTarget = (targetTile-CurrentPosition);

            float minAngle = 180.0f;
            float angleToTest = 0.0f;
            Vector2 dirToGo = -CurrentDirection;
            foreach(Vector2 dirToTest in node.availableDirections)
            {
                angleToTest = Vector2.Angle(AngleToTarget,dirToTest);
                if(angleToTest<minAngle && dirToTest!=(-CurrentDirection))
                {
                    minAngle = angleToTest;
                    dirToGo = dirToTest;
                }
            }

            return dirToGo;
        }

    }
}