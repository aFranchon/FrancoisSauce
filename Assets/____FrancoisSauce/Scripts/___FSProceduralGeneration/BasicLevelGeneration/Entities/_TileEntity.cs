using System.Collections.Generic;
using UnityEngine;

//TODO comment
namespace FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Entities
{
    public class TileEntity : MonoBehaviour
    {
        [Header("Number of entities")]
        public int maxNumber = -1;
        public int minNumber = -1;
        
        [Header("Possible Positions")]
        public List<Vector3> positions = new List<Vector3>();
    }
}