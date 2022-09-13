using UnityEngine;

namespace CCLH
{
    [CreateAssetMenu(fileName = "Sound", menuName = "App/SoundData", order = 0)]
    public class Sound : ScriptableObject
    {
        public string name;
        public AudioClip clip;
        
        [Range(0f,1f)]
        public float volume;

        public bool loop;

        [HideInInspector] public AudioSource source;
    }
}