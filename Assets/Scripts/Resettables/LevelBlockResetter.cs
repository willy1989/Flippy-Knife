using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must be placed on each level block prefab
// It is used by pools to reset a level block element and all of its gameobject children
public class LevelBlockResetter : MonoBehaviour
{
    private Resettable[] resettableChildren;

    private void Awake()
    {
        resettableChildren = GetComponentsInChildren<Resettable>();
    }

    public void ResetChildrenLevelBlocks()
    {
        foreach(Resettable ressettable in resettableChildren)
        {
            ressettable.ResetGameObject();
        }
    }
}
