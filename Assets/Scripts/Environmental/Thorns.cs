using DG.Tweening;
using Eclipse.Audio;
using System;
using UnityEngine;

namespace Eclipse
{
    public class Thorns : MonoBehaviour, IDamageable, IScreenShake
    {
        int m_nrOfDamage = 1;
        Tweener m_tweenScaleUp;
        Tweener m_tweenScaleDown;
        bool m_hasAttackStarted = false;
        const float m_scaleUpTime = 5f;
        const float m_scaleDownTime = 2f;
        [SerializeField] private ParticleSystem m_damageParticles;
        private ParticleSystem m_damageParticlesInstance;

        void Start()
        {
            m_tweenScaleUp.SetLink(gameObject);
            m_tweenScaleDown.SetLink(gameObject);
        }
        public int Attack()
        {
            if (!m_hasAttackStarted)
            {
                // Start scaling
                m_tweenScaleUp = gameObject.transform.DOScale(3f, m_scaleUpTime);
                m_tweenScaleDown?.Kill();
                m_hasAttackStarted = true;
            }

            SpawnDamageParticles();
            AudioEvents.TakeThornDamage();

            return m_nrOfDamage;
        }

        public void StopAttack()
        {
            // Scale back down
            m_tweenScaleUp?.Kill(complete: false);
            m_hasAttackStarted = false;
            m_tweenScaleDown = gameObject.transform.DOScale(1f, m_scaleDownTime);
        }

        void OnDestroy()
        {
            m_tweenScaleDown?.Kill();
            m_tweenScaleUp?.Kill();
        }

        private void SpawnDamageParticles()
        {
            m_damageParticlesInstance = Instantiate(m_damageParticles, transform.position, Quaternion.identity);
        }
    }
}

