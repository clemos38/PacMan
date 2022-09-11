using System;
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
        public Movement Movement { get; private set; }
        public Transform Tf { get; private set; }

        #region States definitions

        public GhostNormalState NormalState { get; private set; } //Scatter mode
        public GhostChaseState ChaseState { get; private set; } //Chase pacman
        public GhostWeakState WeakState { get; private set; } //Frighten
        public GhostDeathState DeathState { get; private set; } //Go back home
        public GhostRespawnState RespawnState { get; private set; } //Get out of home

        #endregion

        private void Awake()
        {
            //Init all states
            NormalState = new GhostNormalState(this); //Scatter mode
            ChaseState = new GhostChaseState(this); //Chase mode
            WeakState = new GhostWeakState(this); //being attacked by pacman
            DeathState = new GhostDeathState(this); //Going back home
            RespawnState = new GhostRespawnState(this); //Respawning at the base and getting out

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            
            //Brain affectation
            SetBrain();
            
            Tf = transform;
            Movement = GetComponent<Movement>();

            _pacmanTransform = pacman.transform;
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

        private void Start()
        {
            ChangeState(NormalState); //!Not sure. To see
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

        #region Visual related

        public void SetAnimatorTrigger(int triggerHash) => _animator.SetTrigger(triggerHash);
        public void SetSpriteColor(Color c) => _spriteRenderer.color = c;
        

        #endregion
        #region PacMan Related Getters
        [SerializeField] private Movement pacman;
        private Transform _pacmanTransform;
        public Vector3 GetPacmanPosition() => _pacmanTransform.position;
        public Vector2 GetPacmanDirection() => pacman.CurrentDir;


        #endregion
    }
}