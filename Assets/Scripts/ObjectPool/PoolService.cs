﻿using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public class PoolService
    {
        private static PoolService _instance;
        private Dictionary<string, ObjectPool> _pools;

        public static PoolService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PoolService();
                }

                return _instance;
            }
        }

        private PoolService()
        {
            _pools = new Dictionary<string, ObjectPool>();
        }

        public FXPool FxPool { get; set; }
        public VolumeFXPool VolumeFXPool { get; set; }

        public void AddPool(GameObject prefab)
        {
            _pools[prefab.name] = new ObjectPool(prefab);
        }

        public ObjectPool GetPool(GameObject prefab)
        {
            if (_pools.TryGetValue(prefab.name, out ObjectPool pool))
            {
                return pool;
            }
            else
            {
                AddPool(prefab);
                return _pools[prefab.name];
            }
        }
    }
}