using UnityEngine;
using UnityEngine.Pool;

public class LevelBlockPool : MonoBehaviour
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

    public void ReleaseLevelBlock(GameObject levelBlock)
    {
        objectPool.Release(levelBlock);
    }

    private GameObject InstantiateObject()
    {
        GameObject blockGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        LevelBlocksData levelBlocksData = blockGameObject.GetComponent<LevelBlocksData>();

        if (levelBlocksData == null)
            Debug.LogError("Block level prefabs should all have a levelBlocksData component. This one doesn't. Please add one.");

        levelBlocksData.originPool = this;

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
