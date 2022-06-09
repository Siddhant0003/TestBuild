
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasualGame.Pool
{
    public class PoolManager : MonoBehaviour , IObjectPooling
    {
        public ObjectPoolItem objectPoolItem;
        private List<GameObject> pooledObjects = new List<GameObject>();
        
        public void PopulatePulpit(IGameManager gameManagerListner)
        {
            for (int i = 0; i < objectPoolItem.amountToPool; i++)
            {
                GameObject pool = Instantiate(objectPoolItem.objectToPool, objectPoolItem.parent);
                Pulpit pulpit =  pool.GetComponent<Pulpit>();
                pulpit.Init(this, gameManagerListner);
                pool.SetActive(false);
                pooledObjects.Add(pool);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }

    [System.Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
        public Transform parent;
    }
}


