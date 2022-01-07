using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private ObjectPool<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool<GameObject>(createFunc: InstantiateObject,
                                             actionOnGet: EnableGameObject,
                                             actionOnRelease: DisableGameObject,
                                             actionOnDestroy: DestroyGameObject,
                                             defaultCapacity: 1,
                                             maxSize: 1);
    }

    public GameObject GetLevelBlock()
    {
        return objectPool.Get();
    }

    private GameObject InstantiateObject()
    {
        GameObject blockGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        blockGameObject.SetActive(false);

        return blockGameObject;
    }

    private void EnableGameObject(GameObject blockGameObject)
    {
        blockGameObject.SetActive(true);
    }

    private void DisableGameObject(GameObject blockGameObject)
    {
        blockGameObject.SetActive(false);
    }

    private void DestroyGameObject(GameObject blockGameObject)
    {
        Destroy(blockGameObject);
    }
}
