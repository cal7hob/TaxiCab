using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TinHead_Developer
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelManagerEditor : Editor
    {
        LevelManager m_target;
        GameManager GM_target;


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            m_target = (LevelManager)target;

			if (m_target.InGameUi.LoadingScreen == null  ||  m_target.InGameUi.HelpScreen == null ||m_target.InGameUi.LoadingScreen == null)
			{
				DisplayWarning("Some GameData");
			}
				
          //  SetLevelData();
            if(GUI.changed)
            {
                EditorUtility.SetDirty(m_target);
            }
        }
        public void SetLevelData()
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUILayout.LabelField("", "GameplayLevels", new GUIStyle(GUI.skin.box), GUILayout.MinHeight(30), GUILayout.MaxWidth(320));
                   

					EditorGUILayout.LabelField("", "All Objects are spawned runtime:", new GUIStyle(GUI.skin.box), GUILayout.MinHeight(30), GUILayout.MaxWidth(320));
					GUILayout.Label("LoadingScreen UI", (EditorStyles.centeredGreyMiniLabel), GUILayout.Width(250));
					m_target.InGameUi.LoadingScreen = (GameObject)EditorGUILayout.ObjectField(m_target.InGameUi.LoadingScreen, typeof(GameObject), true, GUILayout.Width(150f));
					GUILayout.Label("Cinematic UI", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(250));
					m_target.InGameUi.Cinematic = (GameObject)EditorGUILayout.ObjectField(m_target.InGameUi.Cinematic, typeof(GameObject), true, GUILayout.Width(150f));
					GUILayout.Label("HelpScreen UI", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(250));
					m_target.InGameUi.HelpScreen = (GameObject)EditorGUILayout.ObjectField(m_target.InGameUi.HelpScreen, typeof(GameObject), true, GUILayout.Width(150f));
					GUILayout.Label("Paused UI", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(250));




                       
                    
                }
            }
            GUILayout.EndVertical();


        }
		public void DisplayWarning(string Name)
		{
			EditorGUILayout.HelpBox(Name + " Reference are null", MessageType.Warning);
		}
    }
}
