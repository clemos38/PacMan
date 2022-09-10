using System;
using UnityEngine;

namespace PacMan
{
    public class PacManStateManager : MonoBehaviour
    {
        private PacManState _currentState;

        #region States definitions

        public PacManNormalState NormalState { get; private set; }
        public PacManSuperState SuperState { get; private set; }

        #endregion

        private void Awake()
        {
            NormalState = new PacManNormalState();
            SuperState = new PacManSuperState();
        }

        private void Start()
        {
            ChangeState(NormalState);
        }

        public void ChangeState(PacManState state)
        {
            _currentState?.EndState();
            _currentState = state;
            _currentState.EnterState();
        }

        private void Update()
        {
            _currentState.UpdateState();
        }
    }
}
