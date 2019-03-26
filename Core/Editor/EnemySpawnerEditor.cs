using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Congehou
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : Editor
    {
        GUIStyle style;
        private void OnEnable()
        {
            style = new GUIStyle ();
            style.richText = true;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(10f);
            GUILayout.Label("<b>Select Prefab</b>",style);
            if (GUILayout.Button("Select Enemy"))
            {
                Selection.activeObject = PrefabUtility.GetCorrespondingObjectFromOriginalSource(((EnemySpawner)target).enemyPool.prefab);
            }
            GUILayout.Space(10f);
            if (GUILayout.Button("Select Bullet"))
            {
                Selection.activeObject = PrefabUtility.GetCorrespondingObjectFromOriginalSource(((EnemySpawner)target).bulletPool.prefab);
            }
        }
    }
}
