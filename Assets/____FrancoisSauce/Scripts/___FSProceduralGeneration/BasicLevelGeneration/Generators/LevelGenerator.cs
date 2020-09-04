using System.Collections.Generic;
using FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Entities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Generators
{
    /// <summary>
    /// Class to handle level creation. To do so, it uses the tiles created by <see cref="TileGenerator"/>.
    /// For the moment only mix up tiles in a binding of isaac way, but will be updated to handle more options
    /// </summary>
    public class LevelGenerator : MonoBehaviour
    {
        /// <summary>
        /// Minimum number of tiles to put in the level
        /// </summary>
        [Tooltip("Minimum number of tiles to put in the level")]
        public int minTileNumber = 0;
        /// <summary>
        /// maximum number of tiles to put in the level
        /// </summary>
        [Tooltip("Maximum number of tiles to put in the level")]
        public int maxTileNumber = 0;
        /// <summary>
        /// Chosen number of tile to put in the level, just to keep track of it in the entire class
        /// </summary>
        private int tileNumber = 0;

        /// <summary>
        /// First tile to put in the level, leave null if you don't care about it
        /// </summary>
        [Tooltip("First tile to put in the level, leave null if you don't care about it")]
        public __Tile firstTile = null;

        /// <summary>
        /// List of all tiles you want the level to be generated with.
        /// </summary>
        [Tooltip("List of all tiles you want the level to be generated with.")]
        public List<__Tile> possibleTiles = new List<__Tile>();//TODO fill it with tiles in the tile folder automatically

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        /// <summary>
        /// Function used to find the position of the new tile to be added to the level. Do not check every conditions here.
        /// </summary>
        /// <param name="newTile">The new <see cref="__Tile"/> to be added</param>
        /// <param name="position">the position to align with, from the first tile</param>
        /// <param name="index">the chosen index, use to find the side to be appended</param>
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

        /// <summary>
        /// The main function to add tiles.
        /// </summary>
        private void AddTile()
        {
            while (true) // Run this as long as there are no tiles added
            {
                if (firstTile == null)
                    firstTile = Instantiate(possibleTiles[Random.Range(0, possibleTiles.Count)]); // if no first tile, create one and exit the function
                else
                {
                    var newTile = Instantiate(possibleTiles[Random.Range(0, possibleTiles.Count)]); // Create new Tile in the level scene
                    var position = firstTile.transform.position;
                    
                                   var index = Random.Range(0, 5);
                    switch (index) //index is here to define the side to be appended
                    {
                        case 0: //Up side
                        {
                            SetNewTilePosition(newTile, position + firstTile.freePositionsUp[Random.Range(0, firstTile.freePositionsUp.Count)], index);

                            if (!firstTile.CheckNewTile(newTile)) //checking if the new tile is okay or not if false try with another new tile
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                        }
                        case 1: //Down side
                            SetNewTilePosition(newTile, position + firstTile.freePositionsDown[Random.Range(0, firstTile.freePositionsDown.Count)], index);

                            if (!firstTile.CheckNewTile(newTile)) //checking if the new tile is okay or not if false try with another new tile
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                        case 2: //Left side
                            SetNewTilePosition(newTile, position + firstTile.freePositionsLeft[Random.Range(0, firstTile.freePositionsLeft.Count)], index);

                            if (!firstTile.CheckNewTile(newTile)) //checking if the new tile is okay or not if false try with another new tile
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                        default: //Right side
                            SetNewTilePosition(newTile, position + firstTile.freePositionsRight[Random.Range(0, firstTile.freePositionsRight.Count)], index);

                            if (!firstTile.CheckNewTile(newTile)) //checking if the new tile is okay or not if false try with another new tile
                            {
                                DestroyImmediate(newTile.gameObject, true);
                                continue;
                            }
                            break;
                    }
                }
                
                break;
            }
        }

        /// <summary>
        /// The function called from unity editor button, start the creation of the new level
        /// </summary>
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
                AddTile();
            }
            
            EditorSceneManager.SaveScene(newScene, localPath);
        }
#endif
#endif
    }
}

