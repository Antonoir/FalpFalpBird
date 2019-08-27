using System;
using UnityEngine;

namespace Game
{
    public class PointGainedSound : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;

        private AudioSource audioSource;
        private PointGainedEventChannel pointGainedEventChannel;

        private void Awake()
        {
            pointGainedEventChannel = Finder.PointGainedEventChannel;
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnEnable()
        {
            pointGainedEventChannel.OnPointGained += PlayPointGainedSound;
        }

        private void OnDisable()
        {
            pointGainedEventChannel.OnPointGained -= PlayPointGainedSound;
        }

        private void PlayPointGainedSound()
        {
            audioSource.PlayOneShot(audioClip);
            Debug.Log("PouCling!! Catho");
        }
    }
}