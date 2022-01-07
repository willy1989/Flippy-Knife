using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlocksData : MonoBehaviour
{
    [SerializeField] private Transform blockEnd;

    public Transform BlockEnd 
    { 
        get 
        { 
            return blockEnd; 
        } 
    }
}

