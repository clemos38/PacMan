using UnityEngine;
using UnityEngine.InputSystem;

namespace CCLH
{
    public class InputController : MonoBehaviour
    {
        public static InputController Singleton;

        private Movement _movement;
        private Controls _action;
        private InputAction _move;

        private void OnEnable()
        {
            if(_action is null)
            {
                _action = new Controls();
                _move = _action.Controles.Movements;
            }
            
            _action.Enable();
        }

        private void OnDisable()
        {
            _action.Disable();
        }

        public void SetActionActive(bool b)
        {
            if(b) _action.Enable();
            else _action.Disable();
        }

        private void Awake()
        {
            if(Singleton != null && Singleton != this) Destroy(gameObject);
            Singleton = this;

            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            LookForInput();
            Rotate();
        }

        private void LookForInput()
        {
            var dir = _move.ReadValue<Vector2>();
            
            //TODO : Pas très opti, modifier et utiliser les prop des Vector2
            if (dir[1] > 0.5)
            {
                _movement.ChangerDirection(Vector2.up);
            }
            else if (dir[1] < -0.5)
            {
                _movement.ChangerDirection(Vector2.down);
            }
            else if (dir[0] > 0.5)
            {
                _movement.ChangerDirection(Vector2.right);
            }
            else if (dir[0] < -0.5)
            {
                _movement.ChangerDirection(Vector2.left);
            }
        }

        //TODO : To move to the movement script (more logical)
        private void Rotate()
        {
            var angle = Mathf.Atan2(_movement.CurrentDir.y, _movement.CurrentDir.x) + Mathf.PI;
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }
        
        
    }
}