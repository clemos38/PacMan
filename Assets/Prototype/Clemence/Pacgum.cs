using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCLH
{
    public class Pacgum : MonoBehaviour
    {
        public int points = 10;
        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _audio.Stop();
            _audio.playOnAwake = false;
            _audio.loop = false;
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                AudioManager.Singleton.PlayPelletSound();
                GetEaten();
            }
        }
        protected virtual void GetEaten()
        {
            GameManager.Singleton.EatPacgum(this);
        }
    }
}
