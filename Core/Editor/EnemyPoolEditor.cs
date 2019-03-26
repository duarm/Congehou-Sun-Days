using UnityEngine;
using UnityEditor;

namespace Congehou
{
    [CustomEditor(typeof(EnemyPool))]
    public class EnemyPoolEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EnemyPool enemySpawner = (EnemyPool)target;
            if(GUILayout.Button("Update Pattern"))
            {
                //enemySpawner.UpdatePattern();
            }
        }
    }
}
