using System;
using System.Collections;
using CCLH;
using UnityEngine;

namespace Ghosts
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GhostStateManager : MonoBehaviour
    {
        private GhostState _currentState;
        public GhostBrain brain { get; private set; }

        [Header("Ghost info")] 
        [SerializeField] private Color color;
        [SerializeField] private GhostType type;
        
        public LayerMask nodeLayer;
        
        
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _collider;
        public Movement Movement { get; private set; }
        public Transform Tf { get; private set; }

        #region States definitions

        public GhostNormalState NormalState { get; private set; } //Scatter mode
        public GhostChaseState ChaseState { get; private set; } //Chase pacman
        public GhostWeakState WeakState { get; private set; } //Frighten
        public GhostDeathState DeathState { get; private set; } //Go back home
        public GhostRespawnState RespawnState { get; private set; } //Get out of home

        #endregion

        private void Start()
        {
            //Init all states
            NormalState = new GhostNormalState(this); //Scatter mode
            ChaseState = new GhostChaseState(this); //Chase mode
            WeakState = new GhostWeakState(this); //being attacked by pacman
            DeathState = new GhostDeathState(this); //Going back home
            RespawnState = new GhostRespawnState(this); //Respawning at the base and getting out

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<CircleCollider2D>();
            
            //Brain affectation
            SetBrain();
            _animationStrengthenHash = Animator.StringToHash("Strengthen");
            
            
            Tf = transform;
            Movement = GetComponent<Movement>();

            _pacmanTransform = pacman.transform;
            
            ChangeState(NormalState);
        }

        private void SetBrain()
        {
            switch (type)
            {
                case GhostType.Red:
                    brain = new RedBrain();
                    break;
                case GhostType.Blue:
                    brain = new BlueBrain();
                    break;
                case GhostType.Purple:
                    brain = new PurpleBrain();
                    break;
                case GhostType.Orange:
                    brain = new OrangeBrain();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        

        private void Update()
        {
            _currentState.UpdateState();
        }

        public void ChangeState(GhostState state)
        {
            _currentState?.EndState();
            _currentState = state;
            _currentState.EnterState();
        }

        private void OnDrawGizmos()
        {
            if (_currentState is null) return;
            _currentState.OnDrawGizmos();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log($"We collide with {col.collider.name}");
            if (col.transform.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                Debug.Log("We collide with pacman.");
                _currentState.OnCollisionEnter();
            } 
        }

        private int _animationStrengthenHash;
        private IEnumerator WeakDurationCoroutine()
        {
            //TODO : Make the timers set in the GameManager so as to be more changeable.
            yield return new WaitForSeconds(10); 
            SetAnimatorTrigger(_animationStrengthenHash);
            yield return new WaitForSeconds(5); 
            ChangeState(NormalState);
        }

        public void StartWeakTimer() => StartCoroutine(WeakDurationCoroutine());
        public void StopWeakTimer() => StopCoroutine(WeakDurationCoroutine());

        public void DisableGhostCollisionWithPacMan(bool b)
        {
            _collider.isTrigger = b;
        }
        
        #region Visual related

        public void SetAnimatorTrigger(int triggerHash) => _animator.SetTrigger(triggerHash);
        public void SetSpriteColor(bool b) => _spriteRenderer.color = b ? color : Color.white;

        public void SetEyesActive(bool b) => Tf.GetChild(0).gameObject.SetActive(b);

        public void SetBodyActive(bool b) => _spriteRenderer.enabled = b;

        #endregion
        
        #region PacMan Related Getters
        [SerializeField] private Movement pacman;
        private Transform _pacmanTransform;
        public int points = 50;
        public Vector3 GetPacmanPosition() => _pacmanTransform.position;
        public Vector2 GetPacmanDirection() => pacman.CurrentDir;
        


        #endregion
    }
}