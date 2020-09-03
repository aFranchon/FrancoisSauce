using System.Collections.Generic;
using System.ComponentModel;
using FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Entities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

//TODO comment
namespace FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        public int minTileNumber = 0;
        public int maxTileNumber = 0;
        private int tileNumber = 0;

        public __Tile firstTile = null;

        public List<__Tile> possibleTiles = new List<__Tile>();//TODO fill it with tiles in the tile folder automatically

#if UNITY_EDITOR
#if ODIN_INSPECTOR


        private static void SetNewTilePosition(__Tile newTile, Vector3 position, int index)
        {
            Vector3 positionToAlign;

            switch (index)
            {
                case 0:
                    positionToAlign = newTile.freePositionsDown[Random.Range(0, newTile.freePositionsDown.Count)];
                    break;
                case 1:
                    positionToAlign = newTile.freePositionsUp[Random.Range(0, newTile.freePositionsUp.Count)];
                    break;
                case 2:
                    positionToAlign = newTile.freePositionsRight[Random.Range(0, newTile.freePositionsRight.Count)];
                    break;
                default:
                    positionToAlign = newTile.freePositionsLeft[Random.Range(0, newTile.freePositionsLeft.Count)];
                    break;
            }

            var transform1 = newTile.transform;
            transform1.position = position - (transform1.position + positionToAlign);
        }

        private void AddTile(Scene newScene)
        {
            while (true)
            {
                if (firstTile == null)
                    firstTile = Instantiate(possibleTiles[Random.Range(0, possibleTiles.Count)]);
                else
                {
                    var newTile = Instantiate(possibleTiles[Random.Range(0, possibleTiles.Count)]);
                    var position = firstTile.transform.position;
                    
                                   var index = Random.Range(0, 5);
                    switch (index)
                    {
                        case 0:
                        {
                            SetNewTilePosition(newTile, position + firstTile.freePositionsUp[Random.Range(0, firstTile.freePositionsUp.Count)], index);

                            if (!firstTile.CheckNewTile(newTile))
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                        }
                        case 1:
                            SetNewTilePosition(newTile, position + firstTile.freePositionsDown[Random.Range(0, firstTile.freePositionsDown.Count)], index);

                            if (!firstTile.CheckNewTile(newTile))
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                        case 2:
                            SetNewTilePosition(newTile, position + firstTile.freePositionsLeft[Random.Range(0, firstTile.freePositionsLeft.Count)], index);

                            if (!firstTile.CheckNewTile(newTile))
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                        default:
                            SetNewTilePosition(newTile, position + firstTile.freePositionsRight[Random.Range(0, firstTile.freePositionsRight.Count)], index);

                            if (!firstTile.CheckNewTile(newTile))
                            {                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                    }
                }
                
                break;
            }
        }

        [Button("Create Level")]
        [ContextMenu("Create Level")]
        public void CreateLevel()
        {
            var localPath = "Assets/___EXEMPLEGAME_Change this name/Levels/Level0001.unity";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            
            var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);

            tileNumber = Random.Range(minTileNumber, maxTileNumber + 1);

            for (var i = 0; i < tileNumber; i++)
            {
                AddTile(newScene);
            }
            
            EditorSceneManager.SaveScene(newScene, localPath);
        }
#endif
#endif
    }
}

