using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCLH
{
    public class Pacgum : MonoBehaviour
    {
        public int points = 10;
        

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                GetEaten();
            }
        }
        protected virtual void GetEaten()
        {
            GameManager.Singleton.EatPacgum(this);
        }
    }
}
