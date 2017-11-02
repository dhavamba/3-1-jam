using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public static class StaticPool
    {
        private static Dictionary<string, GameObject> listPool = new Dictionary<string, GameObject>();

        /// <summary>
        /// Create/Recycle element for specific Pool
        /// </summary>
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent = null, float max = Mathf.Infinity)
        {
            return GetPool(prefab, max).InstantiateObject(parent, position);
        }

        /// <summary>
        /// Destroy/Disable for specific Pool
        /// </summary>
        public static void Destroy(GameObject obj)
        {
            GetPool(obj).Disable(obj);
        }


        /// <summary>
        /// Find specific Pool for obj
        /// </summary>
        public static SystemPool GetPool(GameObject obj, float max = Mathf.Infinity)
        {
            string name = obj.name.Split('(')[0];
            if (max <= 0)
            {
                Debug.LogError("The number of Pool is zero or negative");
                return null;
            }
            if (obj == null)
            {
                Debug.LogError("The prefab is null");
                return null;
            }

            if (listPool.ContainsKey(name))
            {
                return listPool[name].GetComponent<SystemPool>();
            }
            else
            {
                GameObject pr = GameObject.Find("Pools");
                if (!pr)
                {
                    pr = new GameObject("Pools");
                }


                GameObject poolContainer = new GameObject(obj.name + "Pool");
                poolContainer.transform.parent = pr.transform;
                SystemPool pool = poolContainer.AddComponent<SystemPool>();
                listPool[obj.name] = poolContainer;
                pool.max = max;
                pool.Prefab = obj;
                return pool;
            }
        }

        /// <summary>
        /// Remove specific Pool for obj
        /// </summary>
        public static void RemovePool(GameObject obj)
        {
            if (listPool.ContainsKey(obj.name))
            {
                GameObject.Destroy(listPool[obj.name]);
                listPool.Remove(obj.name);
            }
        }

        /// <summary>
        /// Remove all Pools
        /// </summary>
        public static void RemoveAllPool()
        {
            listPool.Clear();
        }
    }

