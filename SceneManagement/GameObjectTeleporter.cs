using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Congehou
{
    /// <summary>
    /// This class is used to move gameobjects from one position to another in the scene.
    /// </summary>
    public class GameObjectTeleporter : MonoBehaviour
    {
        public static GameObjectTeleporter Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                instance = FindObjectOfType<GameObjectTeleporter>();

                if (instance != null)
                    return instance;

                GameObject gameObjectTeleporter = new GameObject("GameObjectTeleporter");
                instance = gameObjectTeleporter.AddComponent<GameObjectTeleporter>();

                return instance;
            }
        }

        public static bool Transitioning
        {
            get { return Instance.m_Transitioning; }
        }

        protected static GameObjectTeleporter instance;

        protected PlayerCharacter m_PlayerInput;
        protected bool m_Transitioning;

        void Awake ()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            m_PlayerInput = FindObjectOfType<PlayerCharacter>();
        }

        public static void Teleport (GameObject transitioningGameObject, Transform destination)
        {
            Instance.StartCoroutine (Instance.Transition (transitioningGameObject, false, false, destination.position, false));
        }

        public static void Teleport (GameObject transitioningGameObject, Vector3 destinationPosition)
        {
            Instance.StartCoroutine (Instance.Transition (transitioningGameObject, false, false, destinationPosition, false));
        }

        protected IEnumerator Transition (GameObject transitioningGameObject, bool releaseControl, bool resetInputValues, Vector3 destinationPosition, bool fade)
        {
            m_Transitioning = true;

            if (releaseControl)
            {
                if (m_PlayerInput == null)
                    m_PlayerInput = FindObjectOfType<PlayerCharacter> ();
                m_PlayerInput.DisableInput ();
            }

            if(fade)
                yield return StartCoroutine (ScreenFader.FadeSceneOut ());

            transitioningGameObject.transform.position = destinationPosition;
        
            if(fade)
                yield return StartCoroutine (ScreenFader.FadeSceneIn ());

            if (releaseControl)
            {
                m_PlayerInput.EnableInput ();
            }

            m_Transitioning = false;
        }
    }
}