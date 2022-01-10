using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Level blocks are prefabs, which are used as blueprints by the level block pools.
public class LevelBlocksData : MonoBehaviour
{
    [SerializeField] private Transform blockEnd;

    // We need to keep track of which pool this levelBlock belongs to,
    // because the levelBuilder keeps a list of blocks that are in the scene,
    // so that they can be released by their corresponding pool.

    public LevelBlockPool originPool;

    public Transform BlockEnd 
    { 
        get 
        { 
            return blockEnd; 
        } 
    }
}

