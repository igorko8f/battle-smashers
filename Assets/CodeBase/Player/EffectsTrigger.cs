using UnityEngine;

namespace CodeBase.Player
{
    public class EffectsTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem moveParticles;
        [SerializeField] private AudioSource audioSource;
        
        public virtual void PlayEffects()
        {
            if (moveParticles.isPlaying == false || moveParticles.isEmitting == false)
            {
                moveParticles.Play();
                audioSource.Play();
            }
        }

        public virtual void StopEffects()
        {
            if (moveParticles.isPlaying || moveParticles.isEmitting)
            {
                moveParticles.Stop();
                audioSource.Stop();
            }
        }
    }
}