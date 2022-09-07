using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPacgum : Pacgum
{
    public int duration = 1;
   
    protected override void GetEaten()
    {
        FindObjectOfType<GameManager>().EatSuperPacgum(this);
    }
}
