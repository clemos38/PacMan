using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float vitesse = 8.0f;
    public float facteurVit = 1.0f;

    public float boxcastSize = 0.8f;

    public Vector2 directionInitiale;
    public Vector2 directionActuelle {get; private set; }
    public Vector2 directionSuivante {get; private set; }

    public Vector3 positionDépart {get; private set; }

    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if(directionSuivante != Vector2.zero)
        {
            ChangerDirection(directionSuivante);
        }
    }

    public void Reset()
    {
        facteurVit = 1.0f;
        directionActuelle = directionInitiale;
        directionSuivante = Vector2.zero;
        transform.position = positionDépart;
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 difference = directionActuelle*vitesse*facteurVit*Time.fixedDeltaTime;
        rigidbody.MovePosition(position + difference);
    }

    public void ChangerDirection(Vector2 Ndirection)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * boxcastSize, 0.0f, Ndirection, 1.5f, obstacleLayer);
        if(hit.collider == null)
        {
            directionActuelle = Ndirection;
            directionSuivante = Vector2.zero;
        }
        else
        {
            directionSuivante = Ndirection;
        }
    }
}
