using UnityEngine;

namespace CCLH
{
    public class Pacman : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private new Collider2D collider;
        private Movement movement;
        public bool isPassingTunnel;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
            movement = GetComponent<Movement>();
            isPassingTunnel = false;
        }

        public void ResetState()
        {
            enabled = true;
            spriteRenderer.enabled = true;
            collider.enabled = true;
            movement.Reset();
            movement.enabled = true;
            gameObject.SetActive(true);
        }

        public void DeathSequence()
        {
            enabled = false;
            spriteRenderer.enabled = false;
            collider.enabled = false;
            movement.enabled = false;
        }
    }
}
