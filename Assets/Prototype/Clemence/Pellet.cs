using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            GetEaten();
        }
    }
    protected virtual void GetEaten()
    {
        FindObjectOfType<GameManager>().EatPellet(this);
    }
}
