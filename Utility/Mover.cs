using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class Mover : MonoBehaviour, ITimeObject
    {
        public Vector3 velocity;
        public float sectionCallMultiplier;

        public bool m_AffectedByTime = true;

        private bool m_TimeStopped = false;
        private bool m_SpeededUp = false; 

        private void OnEnable()
        {
            if(m_AffectedByTime)
                TimeManager.RegisterTimeAffectedObject(this);
        }

        private void OnDisable()
        {
            if(m_AffectedByTime)
                TimeManager.UnregisterTimeAffectedObject(this);
        }

        void Update()
        {
            if(!m_TimeStopped)
                transform.position += velocity * Time.deltaTime;
        }

        public void OnTimeFlow()
        {
            m_TimeStopped = false;
        }

        public void OnTimeStop()
        {
            m_TimeStopped = true;
        }

        public void SectionCall()
        {
            if(!m_SpeededUp)
            {
                velocity.y *= sectionCallMultiplier;
                m_SpeededUp = true;
            }
            else
            {
                velocity.y /= sectionCallMultiplier;
                m_SpeededUp = false;
            }
        }
    }
}
