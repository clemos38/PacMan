using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CCLH
{
    public class Pacman : MonoBehaviour
    {
        private Movement _movement;
        private Controls _action;
        private InputAction _move;

        void OnEnable()
        {
            if(_action is null)
            {
                _action = new Controls();
                _move = _action.Controles.Movements;
            }
            
            _action.Enable();
        }

        void OnDisable()
        {
            _action.Disable();
        }


        void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        void Update()
        {
            var dir = _move.ReadValue<Vector2>();
            
            //TODO : Pas très opti, modifier et utiliser les prop des Vector2
            //TODO : Mettre ça dans une méthode externe.
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
            
            
            var angle = Mathf.Atan2(_movement.CurrentDir.y, _movement.CurrentDir.x) + Mathf.PI;
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }
    }
}