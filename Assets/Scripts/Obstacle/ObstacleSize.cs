using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSize : MonoBehaviour
{
    [Header("x : 가로, z :세로, y : 높이")]
    public Vector3Int size;
    [Header("위에 올라갈 수 있는지 없는지에 대한 옵션")]
    public bool canBePlaced;
}
