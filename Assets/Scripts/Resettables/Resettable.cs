using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to reset in space each part of a level block element.
/// When the player cuts or displace the element,
/// it is then reset to its original position and rotation,
/// so that it can be reused when building a new level
/// </summary>

public abstract class Resettable : MonoBehaviour
{
    public abstract void ResetGameObject();
}
