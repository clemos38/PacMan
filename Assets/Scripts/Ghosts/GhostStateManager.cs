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

        float firstcall;
        
        
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

            Tf = transform;
            firstcall = 0;
            Movement = GetComponent<Movement>();

            _pacmanTransform = pacman.transform;
            
            ChangeState(RespawnState);
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

        public float GetRespawnTimer()
        {
            if(firstcall<4)
            {
                firstcall += 1;
                switch (type)
                {
                    case GhostType.Red:
                        return 0f;
                    case GhostType.Orange:
                        return 15f;
                    case GhostType.Purple:
                        return 1f;
                    case GhostType.Blue:
                        return 10f;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                return 5f;
            }
        }

        private void Update()
        {
            _currentState.UpdateState();
        }


        private bool IsTransitionLegal(GhostState current, GhostState state)
        {
            if (current.Equals(RespawnState))
            {
                if (!state.Equals(NormalState) &&  !state.Equals(ChaseState)) return false;
            }
            else if (current.Equals(NormalState))
            {
                if (!state.Equals(WeakState) && !state.Equals(ChaseState)) return false;
            }
            else if (current.Equals(ChaseState))
            {
                if (!state.Equals(WeakState) && !state.Equals(NormalState)) return false;
            }
            else if (current.Equals(WeakState))
            {
                if (state.Equals(RespawnState)) return false;
            }
            else if (current.Equals(DeathState))
            {
                if (!state.Equals(RespawnState)) return false;
            }

            return true;
        }
        public void ChangeStateSpecial(GhostState state)
        {
            //Check for legal transition
            if(!(_currentState is null))
            {
                if (IsTransitionLegal(_currentState, state))
                {
                    _currentState?.EndState();
                    _currentState = state;
                    _currentState.EnterState();
                }
            }
            else
            {
                _currentState?.EndState();
                _currentState = state;
                _currentState.EnterState();
            }
        }

        public void ChangeState(GhostState state)
        {
            //Check for legal transition
            if(!(_currentState is null))
            {
                if (IsTransitionLegal(_currentState, state) && _currentState != RespawnState && _currentState != DeathState)
                {
                    _currentState?.EndState();
                    _currentState = state;
                    _currentState.EnterState();
                }
            }
            else
            {
                _currentState?.EndState();
                _currentState = state;
                _currentState.EnterState();
            }
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

       

        public void DisableGhostCollisionWithPacMan(bool b)
        {
            _collider.isTrigger = b;
        }

        public bool IsWeak() => _currentState.Equals(WeakState);
        

        public Vector2 GetGhostSpawn() => GhostsManager.Singleton.GetGhostSpawn(type);
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