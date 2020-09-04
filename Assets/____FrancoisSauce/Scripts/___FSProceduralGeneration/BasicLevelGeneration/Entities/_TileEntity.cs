using System.Collections.Generic;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSProceduralGeneration.BasicLevelGeneration.Entities
{
    /// <summary>
    /// Class for all entities of <see cref="__Tile"/>
    /// Inherited by them.
    /// Absolutely not working for the moment.
    /// </summary>
    public class TileEntity : MonoBehaviour
    {
        /// <summary>
        /// The max number of entities to be generated
        /// </summary>
        [Tooltip("The max number of entities to be generated")]
        [Header("Number of entities")]
        public int maxNumber = -1;
        /// <summary>
        /// The min number of entities to be generated
        /// </summary>
        [Tooltip("The min number of entities to be generated")]
        public int minNumber = -1;
        
        /// <summary>
        /// Positions the entity can be at
        /// </summary>
        [Tooltip("Positions the entity can be at")]
        [Header("Possible Positions")]
        public List<Vector3> positions = new List<Vector3>();
    }
}