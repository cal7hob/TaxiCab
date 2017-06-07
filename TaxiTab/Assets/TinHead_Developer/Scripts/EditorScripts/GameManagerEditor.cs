using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TinHead_Developer
{
    [System.Serializable]
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        GameManager GameManagePrefab;
        public Texture MolevLogo;
		int tempscene;
        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            GameManagePrefab = (GameManager)target;

            SetGameData();


            if (GUI.changed)
            {
                EditorUtility.SetDirty(GameManagePrefab);
            }
        }
        public void DisplayWarning(string Name)
        {
            EditorGUILayout.HelpBox(Name + " Reference are null", MessageType.Warning);
        }
        public void SetGameData()
        {
            //   SerializedProperty Listner = serializedObject.FindProperty("GameData");

            //if(Listner.isInstantiatedPrefab==true)
            //{
            //    //
            //}
            GUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUI.BeginChangeCheck();
                {
                    GUILayout.Label(MolevLogo, EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(100));

                }
                EditorUtility.SetDirty(GameManagePrefab);
                GUILayout.EndVertical();
            }
            GUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUI.BeginChangeCheck();
                {

                    EditorGUILayout.LabelField("", " Game checks:", new GUIStyle(GUI.skin.box), GUILayout.MinHeight(20), GUILayout.MaxWidth(320));
					tempscene = EditorGUILayout.IntField("Total Scene", tempscene, GUILayout.MinHeight(20), GUILayout.MaxWidth(250f));
                    GameManagePrefab.IsCoinBased = EditorGUILayout.Toggle("Is Coin Based Game?", GameManagePrefab.IsCoinBased);
					GameManagePrefab.IsLockedBasedGame = EditorGUILayout.Toggle("Is Locked baseed Game?", GameManagePrefab.IsLockedBasedGame);
					GameManagePrefab.LevelSelectionLockCoinBased = EditorGUILayout.Toggle("LevelSelectionLockCoinBased?", GameManagePrefab.LevelSelectionLockCoinBased, GUILayout.MinHeight(20), GUILayout.MaxWidth(250f));

                    if (GUILayout.Button("Create!"))
                    {
						GameManagePrefab.TotalScene=tempscene;
                        GameManagePrefab.Gameplaylevel = new level[GameManagePrefab.TotalScene];

                    }
                    GUILayout.EndVertical();
                    StartDrawingObject();
                }

            }


        }
        public void StartDrawingObject()
        {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    EditorGUI.BeginChangeCheck();
                    {
                        if (GameManagePrefab.IsCoinBased)
                        {
                            EditorGUILayout.LabelField("", "Level Coins and locks", new GUIStyle(GUI.skin.box), GUILayout.MinHeight(30), GUILayout.MaxWidth(320));
                        }

                        for (int i = 0; i < GameManagePrefab.TotalScene; i++)
                        {
                            GUILayout.BeginVertical(EditorStyles.helpBox);
                            {
                                EditorGUI.BeginChangeCheck();
                                {
                                    try {
										GameManagePrefab.Gameplaylevel[i].Level = (GameObject)EditorGUILayout.ObjectField("Level"+(1+ i).ToString(), GameManagePrefab.Gameplaylevel[i].Level, typeof(GameObject), true, GUILayout.Width(250f));

										GameManagePrefab.Gameplaylevel[i].IsPlayerSpawn = EditorGUILayout.Toggle("Is Player Spawn?", GameManagePrefab.Gameplaylevel[i].IsPlayerSpawn);
										if (GameManagePrefab.Gameplaylevel[i].IsPlayerSpawn)
                                        {
											GameManagePrefab.Gameplaylevel[i].SpawnPoint = (Transform)EditorGUILayout.ObjectField("SpawnPoint", GameManagePrefab.Gameplaylevel[i].SpawnPoint, typeof(Transform), true, GUILayout.Width(300));
                                        }
										GameManagePrefab.Gameplaylevel[i].IsDestination = EditorGUILayout.Toggle("Contains destination?", GameManagePrefab.Gameplaylevel[i].IsDestination);
										if (GameManagePrefab.Gameplaylevel[i].IsDestination)
                                        {
											GameManagePrefab.Gameplaylevel[i].Destination = (Transform)EditorGUILayout.ObjectField("Destination", GameManagePrefab.Gameplaylevel[i].Destination, typeof(Transform), true, GUILayout.Width(300));
                                        }
										GameManagePrefab.Gameplaylevel[i].TimeBased = EditorGUILayout.Toggle("Time based?", GameManagePrefab.Gameplaylevel[i].TimeBased);
										GameManagePrefab.Gameplaylevel[i].Objective = EditorGUILayout.IntField("Objectives", GameManagePrefab.Gameplaylevel[i].Objective, GUILayout.Width(200f));
										if (GameManagePrefab.Gameplaylevel[i].TimeBased) {GameManagePrefab.Gameplaylevel[i].time = EditorGUILayout.IntField("Time",GameManagePrefab.Gameplaylevel[i].time); }
										GameManagePrefab.Gameplaylevel[i].ContainInstructions = EditorGUILayout.Toggle("Contains Instruction?", GameManagePrefab.Gameplaylevel[i].ContainInstructions);
										if (GameManagePrefab.Gameplaylevel[i].ContainInstructions) { GameManagePrefab.Gameplaylevel[i].Instruction = (GameObject)EditorGUILayout.ObjectField(GameManagePrefab.Gameplaylevel[i].Instruction, typeof(GameObject), true, GUILayout.Width(200f)); }

                                        if (GameManagePrefab.IsCoinBased)
                                        {
											GameManagePrefab.Gameplaylevel[i].Coins = EditorGUILayout.IntField("Level-" + (i + 1) + "-coins", GameManagePrefab.Gameplaylevel[i].Coins, GUILayout.Width(200f));
                                        }
                                        if (GameManagePrefab.IsLockedBasedGame)
                                        {
											GameManagePrefab.Gameplaylevel[i].LevelLocked = EditorGUILayout.Toggle("Level-" + (i + 1) + "-Lock", GameManagePrefab.Gameplaylevel[i].LevelLocked, GUILayout.Width(200f));
                                        }
                                    }
                                    catch(System.Exception e)
                                    {

                                    }
                              }
                         }
                            GUILayout.EndVertical();

                        }
                    }
                }
                GUILayout.EndVertical();

        }
    }
}

