
using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostsManager : MonoBehaviour
    {
        #region Singleton declaration

        public static GhostsManager Singleton;

        private void Awake()
        {
            if(Singleton != null && Singleton != this) Destroy(gameObject);

            Singleton = this;
        }

        #endregion

        [Tooltip("Ghosts must be in this order, red, orange, purple and blue.")]
        [SerializeField] private Transform[] ghosts;

        [SerializeField] private Transform[] ghostsSpawn;

        public Vector2[] GetGhostPosition()
        {
            var pos = new Vector2[ghosts.Length];
            for (var i = 0; i < ghosts.Length; i++)
            {
                pos[i] = ghosts[i].position;
            }
            return pos;
        }

        public Vector2 GetGhostSpawn(GhostType type) => ghostsSpawn[(int) type].position;
        
    }
}