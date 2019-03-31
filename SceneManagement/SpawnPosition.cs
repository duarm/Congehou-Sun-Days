using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    public class SpawnPosition : MonoBehaviour
    {
        private SpriteRenderer m_SpriteRenderer;
        private WaitForSeconds m_FlickeringWait;
        private bool m_Flickering = false;

        private void Awake() 
        {
            m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            m_FlickeringWait = new WaitForSeconds(.5f);
        }

        public void StartFlickering(Section section)
        {
            if(!m_Flickering)
                StartCoroutine(Flicker(section));
        }

        protected IEnumerator Flicker(Section section)
        {
            m_Flickering = true;
            float timer = 0f;

            while (timer < 2)
            {
                m_SpriteRenderer.enabled = !m_SpriteRenderer.enabled;
                yield return m_FlickeringWait;
                timer += .5f;
            }
            m_Flickering = false;
        }
    }
}
