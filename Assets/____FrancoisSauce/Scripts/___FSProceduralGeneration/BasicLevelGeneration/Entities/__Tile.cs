using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

//TODO comment
namespace FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Entities
{
    public class __Tile : MonoBehaviour
    {
        public List<Vector3> freePositionsUp = new List<Vector3>();
        public List<Vector3> freePositionsDown = new List<Vector3>();
        public List<Vector3> freePositionsLeft = new List<Vector3>();
        public List<Vector3> freePositionsRight = new List<Vector3>();
        
        public List<Vector3> lockedPositions = new List<Vector3>();
            
        public List<Transform> groundPositions = new List<Transform>();
        
        public float tileUnit = 10;
        
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button("FindFreePositions")]
        [ContextMenu("FindFreePositions")]
#endif
        public void FindFreePositions()
        {
            //TODO add wall handling
            
            freePositionsUp.Clear();
            freePositionsDown.Clear();
            freePositionsLeft.Clear();
            freePositionsRight.Clear();
            
            foreach (var position in groundPositions.Select(groundPosition => groundPosition.position))
            {
                //Up positions
                var newFreePosition = position + Vector3.forward * (tileUnit / 2);

                var toRemove = Vector3.zero;
                if (freePositionsUp.Contains(freePositionsUp.Find((a) =>
                {
                    var returnValue = Vector3.Distance(a, newFreePosition) < .1f;
                    if (returnValue) toRemove = a;
                    return returnValue;
                })))
                {
                    lockedPositions.Add(newFreePosition);
                    freePositionsUp.Remove(newFreePosition);
                    freePositionsUp.Remove(toRemove);
                }
                else
                {
                    freePositionsUp.Add(newFreePosition);
                }
                

                //Down positions
                toRemove = Vector3.zero;
                newFreePosition = position + Vector3.back * (tileUnit / 2);
                if (freePositionsDown.Contains(freePositionsDown.Find((a) =>
                {
                    var returnValue = Vector3.Distance(a, newFreePosition) < .1f;
                    if (returnValue) toRemove = a;
                    return returnValue;
                })))
                {
                    lockedPositions.Add(newFreePosition);
                    freePositionsDown.Remove(newFreePosition);
                    freePositionsDown.Remove(toRemove);
                }
                else
                {
                    freePositionsDown.Add(newFreePosition);
                }
                
                //Left positions
                toRemove = Vector3.zero;
                newFreePosition = position + Vector3.left * (tileUnit / 2);
                if (freePositionsLeft.Contains(freePositionsLeft.Find((a) =>
                {
                    var returnValue = Vector3.Distance(a, newFreePosition) < .1f;
                    if (returnValue) toRemove = a;
                    return returnValue;
                })))
                {
                    lockedPositions.Add(newFreePosition);
                    freePositionsLeft.Remove(newFreePosition);
                    freePositionsLeft.Remove(toRemove);
                }
                else
                {
                    freePositionsLeft.Add(newFreePosition);
                }
                
                //Right positions
                toRemove = Vector3.zero;
                newFreePosition = position + Vector3.right * (tileUnit / 2);
                if (freePositionsRight.Contains(freePositionsRight.Find((a) =>
                {
                    var returnValue = Vector3.Distance(a, newFreePosition) < .1f;
                    if (returnValue) toRemove = a;
                    return returnValue;
                })))
                {
                    lockedPositions.Add(newFreePosition);
                    freePositionsRight.Remove(newFreePosition);
                    freePositionsRight.Remove(toRemove);
                }
                else
                {
                    freePositionsRight.Add(newFreePosition);
                }
            }

            List<Vector3> tmp1;
            tmp1 = new List<Vector3>();
            var tmp = freePositionsUp.Where(vector3 => freePositionsDown.Contains(freePositionsDown.Find((a) =>
            {
                var returnValue = Vector3.Distance(a, vector3) < .1f;
                if (returnValue) tmp1.Add(a);
                return returnValue;
            }))).ToList();

            for (var i = 0; i < tmp1.Count; i++)
            {
                freePositionsUp.Remove(tmp[i]);
                freePositionsUp.Remove(tmp1[i]);
                freePositionsDown.Remove(tmp[i]);
                freePositionsDown.Remove(tmp1[i]);
                lockedPositions.Add(tmp[i]);
            }
            
            tmp1 = new List<Vector3>();
            tmp = freePositionsLeft.Where(vector3 => freePositionsRight.Contains(freePositionsRight.Find((a) =>
            {
                var returnValue = Vector3.Distance(a, vector3) < .1f;
                if (returnValue) tmp1.Add(a);
                return returnValue;
            }))).ToList();

            for (var i = 0; i < tmp1.Count; i++)
            {
                freePositionsLeft.Remove(tmp[i]);
                freePositionsLeft.Remove(tmp1[i]);
                freePositionsRight.Remove(tmp[i]);
                freePositionsRight.Remove(tmp1[i]);
                lockedPositions.Add(tmp[i]);
            }
        }
#endif

        public bool CheckNewTile(__Tile newTileToCheck)
        {
            //There will be only on real tile on the level manager, all the tiles will be appended to the first one.
            newTileToCheck.FindFreePositions();
            
            //TODO change this to not use contains anymore
            if (newTileToCheck.freePositionsUp.Any(testPosition => lockedPositions.Contains(testPosition)))
                return false;
            if (newTileToCheck.freePositionsDown.Any(testPosition => lockedPositions.Contains(testPosition)))
                return false;
            if (newTileToCheck.freePositionsLeft.Any(testPosition => lockedPositions.Contains(testPosition)))
                return false;
            if (newTileToCheck.freePositionsRight.Any(testPosition => lockedPositions.Contains(testPosition)))
                return false;

            //TODO change this to not use contains anymore
            if (freePositionsUp.Any(testPosition => newTileToCheck.lockedPositions.Contains(testPosition)))
                return false;
            if (freePositionsDown.Any(testPosition => newTileToCheck.lockedPositions.Contains(testPosition)))
                return false;
            if (freePositionsLeft.Any(testPosition => newTileToCheck.lockedPositions.Contains(testPosition)))
                return false;
            if (freePositionsRight.Any(testPosition => newTileToCheck.lockedPositions.Contains(testPosition)))
                return false;

            foreach (var groundPosition in newTileToCheck.groundPositions)
            {
                groundPositions.Add(groundPosition);
            }

            FindFreePositions();
            
            return true;
        }

    }
}