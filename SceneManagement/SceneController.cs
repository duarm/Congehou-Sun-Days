using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Congehou
{
    /// <summary>
    /// This class is used to transition between scenes. This includes triggering all the things that need to happen on transition such as data persistence.
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        #region Singleton
        public static SceneController Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                instance = FindObjectOfType<SceneController>();

                if (instance != null)
                    return instance;

                Create ();

                return instance;
            }
        }

        protected static SceneController instance;

        public static SceneController Create ()
        {
            GameObject sceneControllerGameObject = new GameObject("SceneController");
            instance = sceneControllerGameObject.AddComponent<SceneController>();

            return instance;
        }
        #endregion

        /// <summary>
        /// True if is transitioning
        /// </summary>
        public static bool Transitioning
        {
            get { return Instance.m_Transitioning; }
        }

        public static int CharacterIndex
        {
            get { return Instance.m_CharacterIndex; }
        }

        public bool DEBUGGING = false;
        public InputMaster inputController;

        protected Scene m_CurrentZoneScene;
        protected bool m_Transitioning;
        protected int m_CharacterIndex;
        
        void Awake()
        {
            Application.targetFrameRate = 60;
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>

        public static void RestartZone(bool resetHealth = true)
        {
            if(resetHealth && PlayerCharacter.PlayerInstance != null)
            {
                PlayerCharacter.PlayerInstance.damageable.SetHealth(PlayerCharacter.PlayerInstance.damageable.startingHealth);
            }

            Instance.StartCoroutine(Instance.Transition(Instance.m_CurrentZoneScene.name));
        }

        public static void RestartZoneWithDelay(float delay, bool resetHealth = true)
        {
            Instance.StartCoroutine(CallWithDelay(delay, RestartZone, resetHealth));
        }

        public static void TransitionToScene(string newSceneName)
        {
            Instance.StartCoroutine(Instance.Transition(newSceneName));
        }

        public static void TransitionToScene(int newSceneIndex)
        {
            Instance.StartCoroutine(Instance.Transition(newSceneIndex));
        }

        public static void WinGame()
        {
            Instance.StartCoroutine(Instance.WinGameCoroutine());
        }

        protected IEnumerator WinGameCoroutine()
        {
            m_Transitioning = true;
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.End));

            inputController.Gameplay.Disable();
            inputController.Gameplay.SetCallbacks(null);
            inputController.Menu.Enable();

            yield return SceneManager.LoadSceneAsync("Menu");
            yield return new WaitForSeconds(2);
            
            yield return StartCoroutine(ScreenFader.FadeSceneIn());
            m_Transitioning = false;
        }

        //By Scene name
        protected IEnumerator Transition(string newSceneName)
        {
            m_Transitioning = true;
            
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
            if(newSceneName.Equals("DevRoom"))
            {
                inputController.Menu.Disable();
                inputController.Menu.SetCallbacks(null);
                inputController.Gameplay.Enable();
            }
            else if(newSceneName.Equals("Menu"))
            {
                inputController.Gameplay.Disable();
                inputController.Gameplay.SetCallbacks(null);
                inputController.Menu.Enable();
            }

            yield return SceneManager.LoadSceneAsync(newSceneName);
            
            yield return StartCoroutine(ScreenFader.FadeSceneIn());
            m_Transitioning = false;
        }

        //By Scene index
        protected IEnumerator Transition(int newSceneIndex)
        {
            m_Transitioning = true;
            
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
            yield return SceneManager.LoadSceneAsync(newSceneIndex);
            
            yield return StartCoroutine(ScreenFader.FadeSceneIn());
            LevelManager.Instance.enabled = true;
            m_Transitioning = false;
        }

        static IEnumerator CallWithDelay<T>(float delay, Action<T> call, T parameter)
        {
            yield return new WaitForSeconds(delay);
            call(parameter);
        }

        public string GetCurrentSceneName()
        {
            return m_CurrentZoneScene.name;
        }

        public void SetCharacterIndex(int index)
        {
            m_CharacterIndex = index;
        }
    }
}