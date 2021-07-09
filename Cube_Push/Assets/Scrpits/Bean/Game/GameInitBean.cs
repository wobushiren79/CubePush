using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "资源脚本/GameInit")]
public class GameInitBean : ScriptableObject
{
    [Header("方块容器初始角度")]
    public Vector3 angleForInitContainerAngle= new Vector3(0,30,0);
    [Header("方块初始化位置大小")]
    public float sizeForInitCube = 10;
    [Header("方块初始化缩放时间")]
    public float timeForInitScaleCube = 1;
    [Header("方块初始化移动时间")]
    public float timeForInitMoveCube = 1;

    [Header("方块移除时间")]
    public float timeForRemoveCube;

    [Header("方块旋转速度")]
    public float speedForRotate = 50;

    [Header("方块移动速度")]
    public float speedForMove = 1;

    [Header("方块抖动时间")]
    public float timeForCubeShake = 0.2f;
    [Header("方块抖动强度")]
    public float strengthForCubeShake = 0.1f;
    [Header("方块抖动颤度")]
    public int vibratoForCubeShake = 100;
    [Header("方块抖动随机性")]
    public float randomnessForCubeShake = 100;

    [Header("方块缓动时间")]
    public float timeForCubeMoveStop = 0.05f;
    [Header("方块缓动距离倍数")]
    public float sizeForCubeMoveStop = 0.1f;
    [Header("方块缓动时间延迟")]
    public float timeForCubeMoveStopDelay = 0.05f;
}