using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils
{
    /// <summary>
    /// ObjectPool design pattern really simple implementation. Only allow <see cref="GameObject"/>.
    /// </summary>
    public class GameObjectPool : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// <see cref="List{T}"/> of pool's <see cref="GameObject"/>.
        /// </summary>
        private List<GameObject> pool;

        /// <summary>
        /// The pollContainer transform, to parent the instated pool objects.
        /// </summary>
        [Tooltip("The parent gameObject for the pool objects")]
        [SerializeField] private Transform poolContainer = null;
        /// <summary>
        /// The population's prefab, for instantiation.
        /// </summary>
        [Tooltip("The prefab of the pool objects")]
        [SerializeField] protected GameObject populationPrefab = null;
        /// <summary>
        /// The number of gameObject to create <see cref="GameObject"/>.
        /// </summary>
        [Tooltip("The base number of pool objects (aka number of objects initialized when the game start)")]
        [SerializeField] private int basePopulation = 0;

        #endregion

        #region Initialize Object

        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Start method.
        /// </summary>
        protected virtual void Start()
        {
            pool = GeneratePool();
        }

        /// <summary>
        /// Method to create a <see cref="List{T}"/> of <see cref="GameObject"/> and return it.
        /// </summary>
        /// <returns>The newly created pool of <see cref="GameObject"/></returns>
        private List<GameObject> GeneratePool()
        {
            List<GameObject> newPool = new List<GameObject>();
            GameObject newPopulation;

            for (var i = 0; i < basePopulation; i++)
            {
                newPopulation = Instantiate(populationPrefab, poolContainer);
                newPopulation.SetActive(false);
                newPool.Add(newPopulation);
            }

            return newPool;
        }

        #endregion

        #region Pool methods implementations

        /// <summary>
        /// Method to request a inactive <see cref="GameObject"/> in the pool and return it.
        /// If no GameObjects inactive, then the pool create a new one and return it.
        /// </summary>
        /// <returns>The first inactive <see cref="GameObject"/> in the pool</returns>
        public GameObject RequestPool()
        {
            //finding an elem of the list that is free
            foreach (var population in pool.Where(population => !population.gameObject.activeInHierarchy))
            {
                population.SetActive(true);
                return population;
            }

            //if Any elem are free, just create and add one new to the list and return it

            GameObject newPopulation = Instantiate(populationPrefab, poolContainer);

            pool.Add(newPopulation);

            return newPopulation;
        }

        #endregion
    }
}