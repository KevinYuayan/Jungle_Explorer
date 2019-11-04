using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: PlayerJumpState.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Nov. 3, 2019
/// Description: PlayerJumpState enum used to give descriptive names to each Jump State
/// Revision History:
/// </summary>
namespace Util
{
    [System.Serializable]
    public enum PlayerJumpState
    {
        UP,
        DOWN,
        LAND
    }
}
