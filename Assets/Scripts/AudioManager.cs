using System;
using System.Collections;
using System.Collections.Generic;
using CCLH;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   #region Singleton Declaration

   public static AudioManager Singleton;
   

   #endregion
   
   [Tooltip("pellet sound must be the first one.")]
   [SerializeField] private Sound[] sounds;
   private AudioSource _source;


   private void Awake()
   {
      DontDestroyOnLoad(gameObject);
      if(Singleton != null && Singleton != this) Destroy(gameObject);
      Singleton = this;

      foreach (var sound in sounds)
      {
         sound.source = gameObject.AddComponent<AudioSource>();
         sound.source.clip = sound.clip;
         sound.source.loop = sound.loop;
         sound.source.volume = sound.volume;
         sound.source.playOnAwake = false;
      }
   }

   private void PlaySound(Sound s) => s.source.Play();
   private void StopSound(Sound s) => s.source.Stop();

   public void PlaySound(string soundName, bool b = true)
   {
      var sound = Array.Find(sounds, sound => sound.name == soundName);
      if (sound is null) throw new Exception($"The sound {soundName} has not been found !");
      if(b) PlaySound(sound);
      else StopSound(sound);
   }


   public void PlayPelletSound() => PlaySound(sounds[0]);
}
