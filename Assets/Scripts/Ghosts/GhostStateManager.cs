using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostStateManager : MonoBehaviour
    {
        private GhostState _currentState;
        private GhostBrain _brain;
        [SerializeField] private GhostData data;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        #region States definitions

        public GhostNormalState NormalState { get; private set; }
        public GhostChaseState ChaseState { get; private set; }
        public GhostWeakState WeakState { get; private set; }
        public GhostDeathState DeathState { get; private set; }
        public GhostRespawnState RespawnState { get; private set; }

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
            _brain = data.GetBrain(); //! Make sure GhostBrain in GhostData is loaded beforehand.
        }

        private void Start()
        {
            ChangeState(RespawnState); //!Not sure. To see
        }

        private void Update()
        {
            _currentState.UpdateState(_brain);
        }

        public void ChangeState(GhostState state)
        {
            _currentState?.EndState();
            _currentState = state;
            _currentState.EnterState();
        }

        public void SetAnimatorTrigger(int triggerHash) => _animator.SetTrigger(triggerHash);
        public void SetSpriteColor(Color c) => _spriteRenderer.color = c;
    }
}