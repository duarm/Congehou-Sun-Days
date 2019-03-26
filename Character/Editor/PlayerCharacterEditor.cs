using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Congehou
{
    [CustomEditor(typeof(PlayerCharacter))]
    public class PlayerCharacterEditor : Editor
    {
        SerializedProperty m_OnHomuraUseProp;

        //References Prop
        SerializedProperty m_InputControllerProp;
        SerializedProperty m_SpriteRendererProp;
        SerializedProperty m_HitboxRendererProp;
        SerializedProperty m_HitColliderProp;
        SerializedProperty m_PointColliderProp;
        SerializedProperty m_TimeManagerProp;
        SerializedProperty m_DamageableProp;
        SerializedProperty m_HomuraAnimatorProp;

        //Movement Prop
        SerializedProperty m_HittableLayersProp;
        SerializedProperty m_CanHitTriggersProp;
        SerializedProperty m_AccelerationProp;
        SerializedProperty m_DecelerationProp;
        SerializedProperty m_RunSpeedProp;
        SerializedProperty m_WalkSpeedProp;
        SerializedProperty m_DashSpeedProp;
        SerializedProperty m_DashPressTimeProp;
    
        //Hurt Prop
        SerializedProperty m_FlickeringDurationProp;
        SerializedProperty m_RespawnLocationProp;

    
        //Shoot Prop
        SerializedProperty m_ShotsPerSecondProp;
        SerializedProperty m_BulletSpawnPointLeftProp;
        SerializedProperty m_BulletSpawnPointMiddleProp;
        SerializedProperty m_BulletSpawnPointRightProp;
        SerializedProperty m_BulletPoolLeftProp;
        SerializedProperty m_BulletPoolMiddleProp;
        SerializedProperty m_BulletPoolRightProp;
        SerializedProperty m_LeftPatternProp;
        SerializedProperty m_MiddlePatternProp;
        SerializedProperty m_RightPatternProp;


        //Audio Prop
        SerializedProperty m_GameOverAudioPlayerProp;
        SerializedProperty m_HurtAudioPlayerProp;
        SerializedProperty m_ShootAudioPlayerProp;
        SerializedProperty m_DashAudioPlayerProp;

        //Particle Prop
        SerializedProperty m_DeathParticleProp;
        SerializedProperty m_RespawnParticleProp;

        //Misc Prop
        SerializedProperty m_SpriteOriginallyFacesLeftProp;

        //Foldouts
        bool m_ReferencesFoldout;
        bool m_MovementSettingsFoldout;
        bool m_HurtSettingsFoldout;
        bool m_AudioSettingsFoldout;
        bool m_ParticleSettingsFoldout;
        bool m_MiscSettingsFoldout;

        readonly GUIContent m_OnHomuraUseContent = new GUIContent("On Homura Use");

        //References Content
        readonly GUIContent m_InputControllerContent = new GUIContent("Input Controller");
        readonly GUIContent m_SpriteRendererContent = new GUIContent("Sprite Renderer");
        readonly GUIContent m_HitboxRendererContent = new GUIContent("Hitbox Renderer");
        readonly GUIContent m_DamageableContent = new GUIContent("Damageable");
        readonly GUIContent m_HitColliderContent = new GUIContent("Hit Collider");
        readonly GUIContent m_PointColliderContent = new GUIContent("Point Collider");
        readonly GUIContent m_TimeManagerContent = new GUIContent("Time Manager");
        readonly GUIContent m_HomuraAnimatorContent = new GUIContent("Homura Animator");

        //Movement Content
        readonly GUIContent m_AccelerationContent = new GUIContent("Acceleration");
        readonly GUIContent m_DecelerationContent = new GUIContent("Deceleration");
        readonly GUIContent m_RunSpeedContent = new GUIContent("Run Speed");
        readonly GUIContent m_WalkSpeedContent = new GUIContent("Walk Speed");
        readonly GUIContent m_DashSpeedContent = new GUIContent("Dash Speed");
        readonly GUIContent m_DashPressTimeContent = new GUIContent("Dash Press Time");

        //Hurt Content
        readonly GUIContent m_HittableLayersContent = new GUIContent("Hittable Layers");
        readonly GUIContent m_CanHitTriggersContent = new GUIContent("Can Hit Triggers");
        readonly GUIContent m_RespawnLocationContent = new GUIContent("Respawn Location");
        readonly GUIContent m_FlickeringDurationContent = new GUIContent("Flicking Duration", "When the player is hurt she becomes invulnerable for a short time and the SpriteRenderer flickers on and off to indicate this.  This field is the duration in seconds the SpriteRenderer stays either on or off whilst flickering.  To adjust the duration of invulnerability see the Damageable component.");

        //Audio Content
        readonly GUIContent m_GameOverAudioPlayerContent = new GUIContent("Gane Over Audio Player");
        readonly GUIContent m_HurtAudioPlayerContent = new GUIContent("Hurt Audio Player");
        readonly GUIContent m_ShootAudioPlayerContent = new GUIContent("Homura Audio Player");
        readonly GUIContent m_DashAudioPlayerContent = new GUIContent("Unity Homura Audio Player");

        //VFX Content
        readonly GUIContent m_DeathParticleContent = new GUIContent("Death Particle");
        readonly GUIContent m_RespawnParticleContent = new GUIContent("Respawn Particle");

        //Misc Content
        readonly GUIContent m_SpriteOriginallyFacesLeftContent = new GUIContent("Sprite Originally Faces Left");

        //Foldouts Content
        readonly GUIContent m_ReferencesContent = new GUIContent("References");
        readonly GUIContent m_MovementSettingsContent = new GUIContent("Movement Settings");
        readonly GUIContent m_HurtSettingsContent = new GUIContent("Hurt Settings");
        readonly GUIContent m_AudioSettingsContent = new GUIContent("Audio Settings");
        readonly GUIContent m_ParticleSettingsContent = new GUIContent("Particle Settings");
        readonly GUIContent m_MiscSettingsContent = new GUIContent("Misc Settings");

        void OnEnable ()
        {
            m_OnHomuraUseProp = serializedObject.FindProperty("onHomuraUse");

            //References Properties
            m_InputControllerProp = serializedObject.FindProperty("inputController");
            m_SpriteRendererProp = serializedObject.FindProperty("spriteRenderer");
            m_HitboxRendererProp = serializedObject.FindProperty("hitboxRenderer");
            m_DamageableProp = serializedObject.FindProperty("damageable");
            m_HitColliderProp = serializedObject.FindProperty("hitCollider");
            m_PointColliderProp = serializedObject.FindProperty("pointCollider");
            m_TimeManagerProp = serializedObject.FindProperty("timeManager");
            m_HomuraAnimatorProp = serializedObject.FindProperty("homuraAnimator");

            //Movement Properties
            m_AccelerationProp = serializedObject.FindProperty("acceleration");
            m_DecelerationProp = serializedObject.FindProperty("deceleration");
            m_RunSpeedProp = serializedObject.FindProperty("runSpeed");
            m_WalkSpeedProp = serializedObject.FindProperty("walkSpeed");
            m_DashSpeedProp = serializedObject.FindProperty("dashSpeed");
            m_DashPressTimeProp = serializedObject.FindProperty("dashPressTime");

            //Hurt Properties
            m_HittableLayersProp = serializedObject.FindProperty("hittableLayers");
            m_CanHitTriggersProp = serializedObject.FindProperty("canHitTrigger");
            m_RespawnLocationProp = serializedObject.FindProperty("respawnLocation");
            m_FlickeringDurationProp = serializedObject.FindProperty ("flickeringDuration");

            //Ranged Properties
            m_ShotsPerSecondProp = serializedObject.FindProperty("shotsPerSecond");
            m_BulletSpawnPointLeftProp = serializedObject.FindProperty("bulletSpawnPointLeft");
            m_BulletSpawnPointMiddleProp = serializedObject.FindProperty("bulletSpawnPointMiddle");
            m_BulletSpawnPointRightProp = serializedObject.FindProperty("bulletSpawnPointRight");
            m_BulletPoolLeftProp = serializedObject.FindProperty("bulletPool");
            m_BulletPoolMiddleProp = serializedObject.FindProperty("bulletPool2");
            m_BulletPoolRightProp = serializedObject.FindProperty("bulletPool3");
            m_LeftPatternProp = serializedObject.FindProperty("leftPattern");
            m_MiddlePatternProp = serializedObject.FindProperty("middlePattern");
            m_RightPatternProp = serializedObject.FindProperty("rightPattern");

            //Audio Properties
            m_GameOverAudioPlayerProp = serializedObject.FindProperty("gameOverAudioPlayer");
            m_HurtAudioPlayerProp = serializedObject.FindProperty("hurtAudioPlayer");
            m_ShootAudioPlayerProp = serializedObject.FindProperty("homuraAudioPlayer");
            m_DashAudioPlayerProp = serializedObject.FindProperty("unityHomuraAudioPlayer");

            //Particle Properties
            m_DeathParticleProp = serializedObject.FindProperty("deathParticle");
            m_RespawnParticleProp = serializedObject.FindProperty("spawnParticle");

            //Misc Properties
            m_SpriteOriginallyFacesLeftProp = serializedObject.FindProperty ("spriteOriginallyFacesLeft");
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();

            EditorGUILayout.PropertyField (m_OnHomuraUseProp, m_OnHomuraUseContent);

            EditorGUILayout.BeginVertical (GUI.skin.box);
            EditorGUI.indentLevel++;

            m_ReferencesFoldout = EditorGUILayout.Foldout (m_ReferencesFoldout, m_ReferencesContent);

            if (m_ReferencesFoldout)
            {
                EditorGUILayout.PropertyField (m_InputControllerProp, m_InputControllerContent);
                EditorGUILayout.PropertyField (m_SpriteRendererProp, m_SpriteRendererContent);
                EditorGUILayout.PropertyField (m_HitboxRendererProp, m_HitboxRendererContent);
                EditorGUILayout.PropertyField (m_DamageableProp, m_DamageableContent);
                EditorGUILayout.PropertyField (m_HitColliderProp, m_HitColliderContent);
                EditorGUILayout.PropertyField (m_PointColliderProp, m_PointColliderContent);
                EditorGUILayout.PropertyField (m_TimeManagerProp, m_TimeManagerContent);
                EditorGUILayout.PropertyField (m_HomuraAnimatorProp, m_HomuraAnimatorContent);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical ();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            m_MovementSettingsFoldout = EditorGUILayout.Foldout(m_MovementSettingsFoldout, m_MovementSettingsContent);

            if (m_MovementSettingsFoldout)
            {
                EditorGUILayout.PropertyField(m_AccelerationProp, m_AccelerationContent);
                EditorGUILayout.PropertyField(m_DecelerationProp, m_DecelerationContent);
                EditorGUILayout.PropertyField(m_RunSpeedProp, m_RunSpeedContent);
                EditorGUILayout.PropertyField(m_WalkSpeedProp, m_WalkSpeedContent);
                EditorGUILayout.PropertyField(m_DashSpeedProp, m_DashSpeedContent);
                EditorGUILayout.PropertyField(m_DashPressTimeProp, m_DashPressTimeContent);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            m_HurtSettingsFoldout = EditorGUILayout.Foldout(m_HurtSettingsFoldout, m_HurtSettingsContent);

            if (m_HurtSettingsFoldout)
            {
                EditorGUILayout.PropertyField (m_HittableLayersProp, m_HittableLayersContent);
                EditorGUILayout.PropertyField (m_CanHitTriggersProp, m_CanHitTriggersContent);
                EditorGUILayout.PropertyField (m_RespawnLocationProp, m_RespawnLocationContent);
                EditorGUILayout.PropertyField (m_FlickeringDurationProp, m_FlickeringDurationContent);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            m_AudioSettingsFoldout = EditorGUILayout.Foldout(m_AudioSettingsFoldout, m_AudioSettingsContent);

            if (m_AudioSettingsFoldout)
            {
                EditorGUILayout.PropertyField(m_GameOverAudioPlayerProp, m_GameOverAudioPlayerContent);
                EditorGUILayout.PropertyField(m_HurtAudioPlayerProp, m_HurtAudioPlayerContent);
                EditorGUILayout.PropertyField(m_ShootAudioPlayerProp, m_ShootAudioPlayerContent);
                EditorGUILayout.PropertyField(m_DashAudioPlayerProp, m_DashAudioPlayerContent);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            m_ParticleSettingsFoldout = EditorGUILayout.Foldout(m_ParticleSettingsFoldout, m_ParticleSettingsContent);

            if (m_ParticleSettingsFoldout)
            {
                EditorGUILayout.PropertyField(m_DeathParticleProp, m_DeathParticleContent);
                EditorGUILayout.PropertyField(m_RespawnParticleProp, m_RespawnParticleContent);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            m_MiscSettingsFoldout = EditorGUILayout.Foldout(m_MiscSettingsFoldout, m_MiscSettingsContent);

            if (m_MiscSettingsFoldout)
            {
                EditorGUILayout.PropertyField(m_SpriteOriginallyFacesLeftProp, m_SpriteOriginallyFacesLeftContent);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties ();
        }
    }
}
