using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] levelBlockPrefabs;

    [SerializeField] private GameObject BonusBlocks;

    private List<GameObject> availableBlockPrefabs = new List<GameObject>();

    private List<GameObject> blocksInGame = new List<GameObject>();

    private Vector3 levelStartPosition = Vector3.zero;

    private Vector3 currentBlockSpawnPosition;

    private const int levelBlockNumber = 6;

    private Vector3 spaceBetweenBlocks = new Vector3(5f, 0f, 0f);

    public static LevelBuilder Instance;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }

        currentBlockSpawnPosition = levelStartPosition;
        availableBlockPrefabs = levelBlockPrefabs.ToList();
    }

    public void CreateLevel()
    {
        for(int i = 0; i < levelBlockNumber; i++)
        {
            GameObject spawnedLevelBlock = Instantiate(GetRandomLevelBlock(), currentBlockSpawnPosition, Quaternion.identity);
            blocksInGame.Add(spawnedLevelBlock);

            currentBlockSpawnPosition += spaceBetweenBlocks;
        }

        GameObject spawnedBonusBlock = Instantiate(BonusBlocks, currentBlockSpawnPosition, Quaternion.identity);
        blocksInGame.Add(spawnedBonusBlock);
    }

    private GameObject GetRandomLevelBlock()
    {
        int randomNumber = Random.Range(0, availableBlockPrefabs.Count);

        GameObject randomBlock = availableBlockPrefabs[randomNumber];

        availableBlockPrefabs.RemoveAt(randomNumber);

        return randomBlock;
    }

    public void ResetLevelBuilder()
    {
        foreach(GameObject item in blocksInGame)
        {
            Destroy(item);
        }

        blocksInGame.Clear();

        availableBlockPrefabs.Clear();

        availableBlockPrefabs = levelBlockPrefabs.ToList();

        currentBlockSpawnPosition = levelStartPosition;
    }

}
