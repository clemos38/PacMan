using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float speedFactor = 1.0f;

    [SerializeField] private float boxcastSize = 0.8f;

    public Vector2 initDir;
    public Vector2 CurrentDir {get; private set; } 
    public Vector2 NextDir {get; private set; } // pas sûr qu'on ait besoin que ce soit public.
    private Vector3 _initPos;

    [SerializeField] private LayerMask obstacleLayer;
    private Rigidbody2D _rb;
    private Transform _tf;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tf = transform;
        _initPos = _tf.position;
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if(NextDir != Vector2.zero)
        {
            ChangerDirection(NextDir);
        }
    }

    public void Reset()
    {
        speedFactor = 1.0f;
        CurrentDir = initDir;
        NextDir = Vector2.zero;
        transform.position = _initPos;
    }

    void FixedUpdate()
    {
        Vector2 position = _rb.position;
        Vector2 difference = speed*speedFactor*Time.fixedDeltaTime* CurrentDir;
        _rb.MovePosition(position + difference);
    }

    public void ChangerDirection(Vector2 Ndirection)
    {
        //TODO : A modifier, les valeurs sont pas bonne je pense.
        //! À régler
        var hit = Physics2D.BoxCast(transform.position, Vector2.one * boxcastSize, 0.0f, Ndirection, 1.5f, obstacleLayer);
        if(hit.collider is  null) // Si on ne rencontre rien dans la nouvelle direction
        {
            CurrentDir = Ndirection; //Change de direction
            NextDir = Vector2.zero; //! Je ne comprend pas l'intérêt de cette variable
        }
        else
        {
            NextDir = Ndirection; // ?
        }
    }
}
