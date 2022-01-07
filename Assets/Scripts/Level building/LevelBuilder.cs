using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] AllBlockPrefabs;

    [SerializeField] private GameObject BonusBlocks;

    [SerializeField] private Pool[] levelBlockPools;

    private List<GameObject> availableBlockPrefabs = new List<GameObject>();

    private List<GameObject> blocksInGame = new List<GameObject>();



    private Vector3 levelStartPosition = Vector3.zero;

    private Vector3 currentBlockSpawnPosition;

    private int levelBlockNumber = 4;

    private Vector3 spaceBetweenBlocks = new Vector3(3f, 0f, 0f);

    public static LevelBuilder Instance;

    private float unlockedLevels
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.LevelsUnlocked, 4);
        }

        set
        {
            if(value <= AllBlockPrefabs.Length-1)
                PlayerPrefs.SetFloat(Constants.LevelsUnlocked, value);
        }
    }

    public void IncrementLevelUnlocked()
    {
        unlockedLevels+= 0.25f;
    }

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

        //PlayerPrefs.SetFloat(Constants.LevelsUnlocked, 4f);

        SetAvailableLevelBlocks();
    }

    private void SetAvailableLevelBlocks()
    {
        availableBlockPrefabs.Clear();

        if (unlockedLevels >= 6 && unlockedLevels < 10)
        {
            levelBlockNumber = 5;
        }

        else if (levelBlockNumber >= 10)
        {
            levelBlockNumber = 7;
        }

        for (int i = 0; i < unlockedLevels; i++)
        {
            availableBlockPrefabs.Add(AllBlockPrefabs[i]);
        }
    }

    public void CreateLevel()
    {
        for(int i = 0; i < levelBlockNumber; i++)
        {
            GameObject spawnedLevelBlock = Instantiate(GetRandomLevelBlock(), currentBlockSpawnPosition, Quaternion.identity);

            blocksInGame.Add(spawnedLevelBlock);

            currentBlockSpawnPosition = spawnedLevelBlock.GetComponent<LevelBlocksData>().BlockEnd.position + spaceBetweenBlocks;
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

        SetAvailableLevelBlocks();

        currentBlockSpawnPosition = levelStartPosition;
    }

}
