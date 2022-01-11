using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Levels are procedurally generated.
/// Each level is made of several blocks follwing each other.
/// Blocks are randomly selected when building a level.
/// Once a run is completed by the player, a new level is generated, and so forth and so on.
/// A level always ends with a bonus block.
/// </summary>

public class LevelBuilder : Singleton<LevelBuilder>
{
    [SerializeField] private GameObject bonusBlocks;


    [SerializeField] private LevelBlockPool[] allLevelBlockPools;

    private List<LevelBlockPool> availablePools = new List<LevelBlockPool>();

    private List<LevelBlocksData> blocksInGame = new List<LevelBlocksData>();

    private int levelBlockNumber = 4;


    private Vector3 currentBlockSpawnPosition;

    private Vector3 levelStartPosition = Vector3.zero;

    private Vector3 spaceBetweenBlocks = new Vector3(3f, 0f, 0f);

    private float unlockedLevels
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.LevelsUnlocked, 4);
        }

        set
        {
            if(value <= allLevelBlockPools.Length-1)
                PlayerPrefs.SetFloat(Constants.LevelsUnlocked, value);
        }
    }

    public void IncrementLevelUnlocked()
    {
        unlockedLevels+= 0.25f;
    }

    private void Awake()
    {
        base.Awake();

        currentBlockSpawnPosition = levelStartPosition;

        SetAvailableLevelBlocks();
    }

    // The blocks that make a level have to be unlocked(unlockedLevels).
    // And the player unlocks them gradually the more, he or she plays.
    private void SetAvailableLevelBlocks()
    {
        availablePools.Clear();

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
            availablePools.Add(allLevelBlockPools[i]);
        }
    }

    public void CreateLevel()
    {
        for(int i = 0; i < levelBlockNumber; i++)
        {
            GameObject spawnedLevelBlock = GetRandomLevelBlock();

            spawnedLevelBlock.transform.position = currentBlockSpawnPosition;

            LevelBlocksData levelBlockData = spawnedLevelBlock.GetComponent<LevelBlocksData>();

            blocksInGame.Add(levelBlockData);

            currentBlockSpawnPosition = levelBlockData.BlockEnd.position + spaceBetweenBlocks;
        }

        bonusBlocks.SetActive(true);

        bonusBlocks.transform.position = currentBlockSpawnPosition;
    }

    private GameObject GetRandomLevelBlock()
    {
        int randomNumber = Random.Range(0, availablePools.Count);

        GameObject randomBlock = availablePools[randomNumber].GetLevelBlock();

        availablePools.RemoveAt(randomNumber);

        return randomBlock;
    }

    public void ResetLevelBuilder()
    {
        bonusBlocks.SetActive(false);

        foreach (LevelBlocksData levelBlockData in blocksInGame)
        {
            levelBlockData.originPool.ReleaseLevelBlock(levelBlockData.gameObject);
        }

        blocksInGame.Clear();

        SetAvailableLevelBlocks();

        currentBlockSpawnPosition = levelStartPosition;
    }
}
