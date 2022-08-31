using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public int duration = 1;
   
    protected override void GetEaten()
    {
        FindObjectOfType<GameManager>().EatPowerPellet(this);
    }
}
