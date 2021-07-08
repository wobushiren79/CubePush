using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class CubeBean : BaseBean
{
    //真实坐标
    public Vector3 positionForReal;
    //标记坐标
    public Vector3Int positionForMark;

    //初始化坐标
    public Vector3 positionForRealInit;
    public Vector3Int positionForMarkInit;
    //方向
    public DirectionEnum direction;

}