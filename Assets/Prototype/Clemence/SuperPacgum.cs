using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCLH
{
    public class SuperPacgum : Pacgum
    {
        public int duration = 1;

        protected override void GetEaten()
        {
            GameManager.Singleton.EatSuperPacgum(this);
            Debug.Log("SuperPacgum eaten");
        }
    }
}
