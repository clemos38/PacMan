using UnityEngine;
using CCLH;

public class Tunnel : MonoBehaviour
{
    [SerializeField] private Tunnel tunnelReceiver;
    public bool isLeft;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Movement>() != null)
        {
            if (collision.gameObject.GetComponent<Movement>().CurrentDir == Vector2.right && !isLeft)
            { collision.gameObject.transform.position = tunnelReceiver.transform.position; }
            if (collision.gameObject.GetComponent<Movement>().CurrentDir == Vector2.left && isLeft)
            { collision.gameObject.transform.position = tunnelReceiver.transform.position; }
        }
    }
}
