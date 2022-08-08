using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerEffectsTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem moveParticles;
        [SerializeField] private AudioSource audioSource;
        
        public void PlayEffects()
        {
            if (moveParticles.isPlaying == false || moveParticles.isEmitting == false)
            {
                moveParticles.Play();
                audioSource.Play();
            }
        }

        public void StopEffects()
        {
            if (moveParticles.isPlaying || moveParticles.isEmitting)
            {
                moveParticles.Stop();
                audioSource.Stop();
            }
        }
    }
}