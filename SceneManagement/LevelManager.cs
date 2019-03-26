﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

namespace Congehou
{
    //Level timer controls the level timer, is used to trigger sections and paused when do so
    public class LevelManager : MonoBehaviour
    {
        #region Singleton
        static protected LevelManager s_LevelManager;
        static public LevelManager Instance { get { return s_LevelManager; } }
        #endregion

        //[ReadOnly]
        public float levelTimer = 0.0f;
        
        [Header("Sections")]
        public Section[] sections;

        private Coroutine m_CurrentSectionCoroutine;
        private Section m_CurrentSection;
        private float m_NextSectionTime = 0;
        private bool m_Skipped;
        private bool m_SectionPlaying = false;
        private int m_CurrentSectionIndex = 0;
        private int m_WaveCount;

        //Constants
        const double k_Approximation = 0.1f;

        private void Awake() 
        {
            s_LevelManager = this;
            m_NextSectionTime = sections[0].triggerTime;
        }

        private void Update()
        {
            if(m_SectionPlaying)
                return;
            else
                levelTimer += Time.deltaTime;

            if(levelTimer > (m_NextSectionTime - k_Approximation) && levelTimer < (m_NextSectionTime + k_Approximation))
            {   
                if(m_CurrentSectionIndex == sections.Length)
                    return;

                m_NextSectionTime = levelTimer;
                PlaySection(sections[m_CurrentSectionIndex]);
            }
        }

        private void PlaySection(Section section)
        {
            m_SectionPlaying = true;
            m_CurrentSection = section;
            if(section.type == SectionType.DIALOGUE)
            {
                m_Skipped = false;
                section.dialogue.stopped += EndDialogueSection;
            }
            section.StartSection();
        }

        public void EndSpawning(Section section)
        {
            m_WaveCount++;

            if(m_WaveCount == section.spawners.Count)
            {
                m_WaveCount = 0;
                EndSection();
            }
        }

        public void StopCurrentTimeline()
        {
            if(m_CurrentSection?.dialogue != null && !m_Skipped)
            {
                m_Skipped = true;
                m_CurrentSection.dialogue.time = m_CurrentSection.dialogue.duration - 2f;
            }
        }

        private void EndDialogueSection(PlayableDirector director)
        {
            EndSection();
        }

        private void EndSection()
        {
            m_SectionPlaying = false;
            
            if(m_CurrentSection.onEndSection != null)
                m_CurrentSection.onEndSection.Invoke();

            m_CurrentSectionIndex++;
            if(m_CurrentSectionIndex < sections.Length)
                m_NextSectionTime += sections[m_CurrentSectionIndex].triggerTime;
        }
    }
}