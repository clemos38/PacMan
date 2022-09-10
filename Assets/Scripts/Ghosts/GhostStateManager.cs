using System;
using CCLH;
using UnityEngine;

namespace Ghosts
{
    public class GhostStateManager : MonoBehaviour
    {
        private GhostState _currentState;
        private GhostBrain _brain;
        [SerializeField] private GhostData data;

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
            NormalState = new GhostNormalState();
            ChaseState = new GhostChaseState();
            WeakState = new GhostWeakState();
            DeathState = new GhostDeathState();
            RespawnState = new GhostRespawnState();
            
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
    }
}