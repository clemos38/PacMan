using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float vitesse = 8.0f;
    public float facteurVit = 1.0f;

    public Vector2 directionInitiale;
    public Vector2 directionActuelle {get; private set; };
    public Vector2 directionSuivante {get; private set; };
    public Vector3 directionDépart {get; private set; };

    public LayerMask obstacleLayer;

    public Rigidbody2D rigidbody { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    public void ResetState()
    {
        this.facteurVit = 1.0f;
        this.directionActuelle = this.directionInitiale;
        this.directionSuivante = Vector2.zero;
    }
}
