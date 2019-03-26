using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Congehou
{
    [CustomEditor(typeof(EnemyBehaviour))]
    public class EnemyBehaviourEditor : Editor
    {
        //SerializedProperty m_ContactDamager;
        SerializedProperty m_BulletSpawnPointProp;
        SerializedProperty m_DamageableProp;
        SerializedProperty m_HitColliderProp;
        SerializedProperty m_HittableLayersProp;
        SerializedProperty m_CanHitTriggerProp;

        SerializedProperty m_ShouldShootProp;
        SerializedProperty m_BurstShotProp;
        SerializedProperty m_BurstBulletsProp;
        SerializedProperty m_BurstGapProp;
        SerializedProperty m_ShootingGapProp;

        SerializedProperty m_FlickeringDurationProp;

        private void OnEnable()
        {
            //m_ContactDamager = serializedObject.FindProperty("contactDamager");
            m_BulletSpawnPointProp = serializedObject.FindProperty("bulletSpawnPoint");
            m_DamageableProp = serializedObject.FindProperty("damageable");
            m_HitColliderProp = serializedObject.FindProperty("hitCollider");
            m_HittableLayersProp = serializedObject.FindProperty("hittableLayers");
            m_CanHitTriggerProp = serializedObject.FindProperty("canHitTrigger");

            m_ShouldShootProp = serializedObject.FindProperty("shouldShoot");
            m_BurstShotProp = serializedObject.FindProperty("burstShot");
            m_BurstBulletsProp = serializedObject.FindProperty("burstBullets");
            m_BurstGapProp = serializedObject.FindProperty("burstGap");
            m_ShootingGapProp = serializedObject.FindProperty("shootingGap");

            m_FlickeringDurationProp = serializedObject.FindProperty("flickeringDuration");
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();

            EditorGUILayout.PropertyField(m_BulletSpawnPointProp);

            EditorGUILayout.Space();
            m_ShouldShootProp.boolValue = EditorGUILayout.Toggle("Should Shoot?", m_ShouldShootProp.boolValue);
            if(m_ShouldShootProp.boolValue)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUI.indentLevel++;
                
                EditorGUILayout.PropertyField(m_ShootingGapProp);

                m_BurstShotProp.boolValue = EditorGUILayout.Toggle("Burst Shot?", m_BurstShotProp.boolValue);
                if(m_BurstShotProp.boolValue)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PropertyField(m_BurstBulletsProp);
                    EditorGUILayout.PropertyField(m_BurstGapProp);

                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndVertical ();
                }

                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical ();
            }

            serializedObject.ApplyModifiedProperties ();
        }
    }
}
