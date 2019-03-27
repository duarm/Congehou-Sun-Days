using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class ParticleManager : MonoBehaviour
    {
        public ParticleSystem[] particles;

        private TimeManager m_TimeManager;

        void Start()
        {
            m_TimeManager = PlayerCharacter.PlayerInstance.timeManager;
            m_TimeManager.onStopTime.AddListener(TimeStop);
            m_TimeManager.onFlowTime.AddListener(TimeFlow);
        }

        public void TriggerStorm(float intensity)
        {

            for(int i = 0; i < particles.Length; i++)
            {
                var mainModule = particles[i].main;
                mainModule.startSpeed = intensity;
                var emissionModule = particles[i].emission;
                emissionModule.rateOverTime = intensity*2;
            }
        }

        public void TimeStop()
        {
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i].Pause();
            }
        }

        public void TimeFlow()
        {
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i].Play();
            }
        }
    }
}
