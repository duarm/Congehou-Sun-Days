using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    /// <summary>
    /// Continuously move an object in one direction
    /// </summary>
    public class Mover : MonoBehaviour, ITimeObject
    {
        //public SpriteRenderer m_RendererUpper;
        //public SpriteRenderer m_RendererMiddle;
        //public SpriteRenderer m_RendererBottom;

        public Vector3 velocity;
        public ProceduralBackground proceduralBackground;
        public float sectionCallMultiplier;

        public bool m_AffectedByTime = true;

        private bool m_TimeStopped = false;
        private bool m_SpeededUp = false; 
        private Rigidbody2D m_RigidBody2D;

        private void Start() 
        {
            m_RigidBody2D = GetComponent<Rigidbody2D>();
        }
        
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

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.CompareTag("BottomCollider"))
            {
                proceduralBackground.UpdatePosition(this);
            }
        }

        void FixedUpdate()
        {
            if(!m_TimeStopped)
                m_RigidBody2D.MovePosition(transform.position + (velocity * Time.deltaTime));
        }

        /*public void FadeTree()
        {
            FadeIn(m_RendererUpper);
            FadeIn(m_RendererMiddle);
            FadeIn(m_RendererBottom);
        }

        public void FadeIn(SpriteRenderer renderer)
        {
            StartCoroutine(Fade(renderer));
        }

        IEnumerator Fade(SpriteRenderer renderer)
        {
            Color tempColor = renderer.material.color;
            tempColor.a = 0;
            renderer.material.color = tempColor;

            float fadeSpeed = Mathf.Abs(tempColor.a - 1) / 5f;
            while (!Mathf.Approximately(tempColor.a, 5))
            {
                tempColor.a = Mathf.MoveTowards(tempColor.a, 1,
                    fadeSpeed * Time.deltaTime);
                renderer.material.color = tempColor;
                yield return null;
            }

            tempColor.a = 1;
            renderer.material.color = tempColor;
        }*/

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
