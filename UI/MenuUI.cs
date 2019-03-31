using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

namespace Congehou
{
    //Controls the menu flow
    public class MenuUI : MonoBehaviour, IMenuActions
    {
        [Header("Indexes")]
        public GameObject optionsPanel;
        public GameObject creditsPanel;
        public GameObject quitPanel;

        [Header("Selector")]
        public Transform selector;
        public Vector2 offset;

        [Header("Audio")]
        public AudioSource m_ChangeSource;
        public AudioSource m_SelectSource;
        //Start index is 3
        public int m_SelectedIndex = 3;

        private void Awake() 
        {
            SceneController.Instance.inputController.Menu.SetCallbacks(this);
            SceneController.Instance.inputController.Menu.Enable();
        }

        public void OnEnter(InputAction.CallbackContext context)
        {
            if(ScreenFader.IsFading)
                return;

            m_SelectSource.Play();
            switch(m_SelectedIndex)
            {
                case 3:
                    StartCoroutine(LoadGame());
                    break;
                case 2:
                    break;
                case 1:
                    break;
                case 0:
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                    break;
            }
        }

        IEnumerator LoadGame()
        {
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Black));
            SceneController.TransitionToScene("DevRoom");
        }

        public void OnUp(InputAction.CallbackContext context)
        {
            if(ScreenFader.IsFading)
                return;

            m_ChangeSource.Play();

            if(m_SelectedIndex == 3)
            {
                m_SelectedIndex = 0;
                selector.transform.position -= 3 * (Vector3)offset;
                OnIndexChange();
                return; 
            }

            m_SelectedIndex++;
            selector.transform.position += (Vector3)offset;

            OnIndexChange();
        }

        public void OnDown(InputAction.CallbackContext context)
        {
            if(ScreenFader.IsFading)
                return;

            m_ChangeSource.Play();

            if(m_SelectedIndex == 0)
            {
                m_SelectedIndex = 3;
                selector.transform.position += 3 * (Vector3)offset;
                OnIndexChange();
                return;
            }

            m_SelectedIndex--;
            selector.transform.position -= (Vector3)offset;

            OnIndexChange();
        }

        public void OnIndexChange()
        {
            if(m_SelectedIndex == 2)
            {
                quitPanel.SetActive(false);
                creditsPanel.SetActive(false);
                optionsPanel.SetActive(true);
            }
            else if(m_SelectedIndex == 1)
            {
                optionsPanel.SetActive(false);
                quitPanel.SetActive(false);
                creditsPanel.SetActive(true);
            }
            else if(m_SelectedIndex == 0)
            {
                creditsPanel.SetActive(false);
                optionsPanel.SetActive(false);
                quitPanel.SetActive(true);
            }
            else
            {
                quitPanel.SetActive(false);
                optionsPanel.SetActive(false);
                creditsPanel.SetActive(false);
            }
        }
    }
}
