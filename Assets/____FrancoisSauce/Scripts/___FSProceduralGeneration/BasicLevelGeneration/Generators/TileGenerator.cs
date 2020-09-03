#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System.Collections.Generic;
using System.ComponentModel;
using FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Entities;
using UnityEditor;
using UnityEngine;

//TODO comment
namespace FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Generators
{
    public class TileGenerator : MonoBehaviour
    {
        public GameObject tilePrefab;

        [Header("Enemies settings")]
        public int numberOfEnemies = 1;
        public List<GameObject> possibleEnemies = new List<GameObject>();
        
        [Header("Environments settings")]
        public int numberOfEnvironments = 1;
        public List<GameObject> possibleEnvironments = new List<GameObject>();
        
        [Header("Grounds settings")]
        public int numberOfGrounds = 1;
        public List<GameObject> possibleGrounds = new List<GameObject>();
        
        [Header("Items settings")]
        public int numberOfItems = 1;
        public List<GameObject> possibleItems = new List<GameObject>();
        
        [Header("Walls settings")]
        public int numberOfWalls = 1;
        public List<GameObject> possibleWalls = new List<GameObject>();
        
        //TODO add something to find prefabs automatically
        
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        private void PopulateTile(GameObject tileToPopulate)
        {
            for (var i = 0; i < numberOfEnemies; i++) 
                Instantiate(possibleEnemies[Random.Range(0, possibleEnvironments.Count)], tileToPopulate.transform);
            
            for (var i = 0; i < numberOfEnvironments; i++) 
                Instantiate(possibleEnvironments[Random.Range(0, possibleEnvironments.Count)], tileToPopulate.transform);

            for (var i = 0; i < numberOfGrounds; i++)
            {
                //TODO change to manage multiple grounds
                var tmp = Instantiate(possibleGrounds[Random.Range(0, possibleGrounds.Count)], tileToPopulate.transform);
                tileToPopulate.GetComponent<__Tile>().groundPositions.Add(tmp.transform);
            }
            
            for (var i = 0; i < numberOfItems; i++) 
                Instantiate(possibleItems[Random.Range(0, possibleItems.Count)], tileToPopulate.transform);
            
            for (var i = 0; i < numberOfWalls; i++) 
                Instantiate(possibleWalls[Random.Range(0, possibleWalls.Count)], tileToPopulate.transform);
            
            tileToPopulate.GetComponent<__Tile>().FindFreePositions();
        }
        
        [Button("Create Tile")]
        [ContextMenu("Create Tile")]
        public void CreateTile()
        {
            var tile = Instantiate(tilePrefab);
            PopulateTile(tile);

            var localPath = "Assets/____FrancoisSauce/Prefabs/ProceduralGeneration/BasicLevelGeneration/Tiles/Tile0000.prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            
            PrefabUtility.SaveAsPrefabAsset(tile, localPath);
            
            DestroyImmediate(tile, true);
        }
#endif
#endif
    }
}