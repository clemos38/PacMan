using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCLH;

public class Tunnel : MonoBehaviour
{
    [SerializeField] private Tunnel tunnelReceiver;
    [SerializeField] private Pacman pacman;
    public bool isEntry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if(pacman.isPassingTunnel == false) {
                pacman.isPassingTunnel = true;
                isEntry = true;
                tunnelReceiver.isEntry = false;
                collision.gameObject.transform.position = tunnelReceiver.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if(isEntry == false)
            {
                pacman.isPassingTunnel = false;
            }
        }
    }
}
