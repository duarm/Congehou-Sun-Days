using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    /// <summary>
    /// Orbitates a target
    /// </summary>
    public class Orbitator : MonoBehaviour, ITimeObject
    {
        public GameObject orbitateAround;
        public Vector3 velocity;
        public float smooth;
        public float width;
        public float height;
        public float depth;

        private float m_TimeCounter;
        private Vector3 m_MoveVector;

        private bool m_TimeStopped = false;

        private void OnEnable()
        {
            TimeManager.RegisterTimeAffectedObject(this);
        }

        private void OnDisable()
        {
            TimeManager.UnregisterTimeAffectedObject(this);
        }

        void Update()
        {
            if(m_TimeStopped)
                return;

            m_TimeCounter += Time.deltaTime;
            m_MoveVector.x = Mathf.Cos(m_TimeCounter) * width;
            m_MoveVector.y = Mathf.Sin(m_TimeCounter) * height;
            m_MoveVector.z = Mathf.Sin(m_TimeCounter) * depth;
            transform.position = Vector3.SmoothDamp(transform.position, orbitateAround.transform.position + m_MoveVector, ref velocity, smooth);
        }

        public void OnTimeStop()
        {
            m_TimeStopped = true;
        }

        public void OnTimeFlow()
        {
            m_TimeStopped = false;
        }

        public void SectionCall()
        {}
    }
}
