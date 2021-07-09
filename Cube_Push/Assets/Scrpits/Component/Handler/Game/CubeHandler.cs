using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

public class CubeHandler : BaseHandler<CubeHandler, CubeManager>
{
    public void CreateRandomCube(int xSize, int ySize, int zSize)
    {
        manager.ClearAllCube();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    CubeBean cubeData = new CubeBean();
                    cubeData.positionForMark = new Vector3Int(x, y, z);
                    cubeData.positionForReal = new Vector3(x - (xSize / 2f) + 0.5f, y - (ySize / 2f) + 0.5f, z - (zSize / 2f) + 0.5f);

                    cubeData.positionForMarkInit = cubeData.positionForMark;
                    cubeData.positionForRealInit = cubeData.positionForReal;

                    cubeData.direction = RandomUtil.GetRandomEnum<DirectionEnum>();

                    GameObject objItemCube = Instantiate(manager.containerForCube.gameObject, manager.modelForCube.gameObject);
                    Cube cube = objItemCube.GetComponent<Cube>();
                    cube.SetData(cubeData);

                    manager.AddCube(cube);
                }
            }
        }
    }

    /// <summary>
    /// 方块初始化
    /// </summary>
    /// <param name="callBack"></param>
    public void AnimForCubeInit(Action callBack)
    {
        GameInitBean gameInit = GameHandler.Instance.manager.gameInitData;
        List<Cube> listCube = manager.listCube;
        for (int i = 0; i < listCube.Count; i++)
        {
            Cube itemCube = listCube[i];
            itemCube.transform.localPosition = itemCube.cubeData.positionForReal * gameInit.sizeForInitCube;
            itemCube.transform.localEulerAngles = new Vector3( UnityEngine.Random.Range(0,360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));

            itemCube.transform
                .DOScale(new Vector3(0, 0, 0), gameInit.timeForInitScaleCube)
                .From();
            itemCube.transform
                .DOLocalMove(itemCube.cubeData.positionForReal, gameInit.timeForInitScaleCube)
                .SetDelay(gameInit.timeForInitMoveCube);
            itemCube.transform
                .DOLocalRotate(itemCube.GetDirectionAngle(), gameInit.timeForInitScaleCube)
                .SetDelay(gameInit.timeForInitMoveCube)
                .OnComplete(()=> { callBack?.Invoke(); });
        }
    }

    /// <summary>
    /// 移动方块
    /// </summary>
    /// <param name="cube"></param>
    public void MoveCube(Cube cube)
    {
        //首先获取方向上的所有方块
        manager.GetListCubeByDirection(cube, out List<Cube> listDirectionCube, out Vector3Int closePosition);
        //如果没有方块 则自身移走
        if (CheckUtil.ListIsNull(listDirectionCube))
        {
            bool isRange = manager.CheckRangeCube(cube);
            if (isRange)
            {
                ShakeCube(cube);
            }
            else
            {
                RemoveCube(cube);
            }
        }
        //如果有方块 则移动到最近的方块附近
        else
        {
            NearCube(cube, listDirectionCube, closePosition);
        }
    }

    /// <summary>
    /// 抖动方块
    /// </summary>
    /// <param name="cube"></param>
    public void ShakeCube(Cube cube)
    {
        GameInitBean gameInit = GameHandler.Instance.manager.gameInitData;
        cube.transform.DOKill();
        cube.transform.localPosition = cube.cubeData.positionForReal;
        cube.transform.DOShakePosition(gameInit.timeForCubeShake, gameInit.strengthForCubeShake, gameInit.vibratoForCubeShake, gameInit.randomnessForCubeShake);
    }




    /// <summary>
    /// 靠近方块
    /// </summary>
    /// <param name="cube"></param>
    /// <param name="listDirectionCube"></param>
    public void NearCube(Cube cube, List<Cube> listDirectionCube, Vector3Int closePosition)
    {
        GameInitBean gameInit = GameHandler.Instance.manager.gameInitData;
        //如果就是当前坐标 则只是播一个动画
        if (closePosition == cube.cubeData.positionForMark)
        {
            AnimForMoveStop(cube.cubeData.direction, cube, 0);
            AnimMoveStopForCloseCube(cube);
        }
        //如果不是则需要先移动指定点
        else
        {
            Vector3 moveDir = closePosition - cube.cubeData.positionForMark;
            cube.cubeData.positionForMark = closePosition;
            cube.cubeData.positionForReal = cube.cubeData.positionForReal + moveDir;

            float dis = Vector3.Distance(closePosition, cube.cubeData.positionForMark);
            cube.transform.DOLocalMove(cube.cubeData.positionForReal, dis / gameInit.speedForMove).OnComplete(() =>
              {
                  AnimMoveStopForCloseCube(cube);
              });
        }
    }


    /// <summary>
    /// 移除方块
    /// </summary>
    /// <param name="cube"></param>
    public void RemoveCube(Cube cube)
    {
        manager.RemoveCube(cube);
        Vector3 movePosition = Vector3.zero;
        switch (cube.cubeData.direction)
        {
            case DirectionEnum.Up:
                movePosition = cube.cubeData.positionForReal + new Vector3(0, 10, 0);
                break;
            case DirectionEnum.Down:
                movePosition = cube.cubeData.positionForReal + new Vector3(0, -10, 0);
                break;
            case DirectionEnum.Left:
                movePosition = cube.cubeData.positionForReal + new Vector3(-10, 0, 0);
                break;
            case DirectionEnum.Right:
                movePosition = cube.cubeData.positionForReal + new Vector3(10, 0, 0);
                break;
            case DirectionEnum.Forward:
                movePosition = cube.cubeData.positionForReal + new Vector3(0, 0, -10);
                break;
            case DirectionEnum.Back:
                movePosition = cube.cubeData.positionForReal + new Vector3(0, 0, 10);
                break;
        }

        GameInitBean gameInit = GameHandler.Instance.manager.gameInitData;
        //本地坐标转世界坐标
        movePosition = manager.containerForCube.TransformPoint(movePosition);

        cube.transform.DOScale(new Vector3(0, 0, 0), gameInit.timeForRemoveCube / 2f).SetDelay(gameInit.timeForRemoveCube / 2f);
        cube.transform.DOMove(movePosition, gameInit.timeForRemoveCube).OnComplete(() =>
        {
            Destroy(cube.gameObject);
        });
    }
    protected void AnimMoveStopForCloseCube(Cube cube)
    {
        GameInitBean gameInit = GameHandler.Instance.manager.gameInitData;
        Vector3Int markPosition = cube.cubeData.positionForMark;
        int timeForDelay = 1;

        while (manager.GetCubeByMarkPosition(markPosition) != null)
        {
            Cube targetCube = manager.GetCubeByMarkPosition(markPosition);
            if (targetCube != cube)
            {
                AnimForMoveStop(cube.cubeData.direction, targetCube, timeForDelay * gameInit.timeForCubeMoveStopDelay);
            }
            switch (cube.cubeData.direction)
            {
                case DirectionEnum.Up:
                    markPosition += new Vector3Int(0, 1, 0);
                    break;
                case DirectionEnum.Down:
                    markPosition += new Vector3Int(0, -1, 0);
                    break;
                case DirectionEnum.Left:
                    markPosition += new Vector3Int(-1, 0, 0);
                    break;
                case DirectionEnum.Right:
                    markPosition += new Vector3Int(1, 0, 0);
                    break;
                case DirectionEnum.Forward:
                    markPosition += new Vector3Int(0, 0, -1);
                    break;
                case DirectionEnum.Back:
                    markPosition += new Vector3Int(0, 0, 1);
                    break;
            }
            timeForDelay++;
        }
    }

    /// <summary>
    /// 动画卡住
    /// </summary>
    /// <param name="cube"></param>
    public void AnimForMoveStop(DirectionEnum direction, Cube cube, float timeForDelay)
    {
        GameInitBean gameInit = GameHandler.Instance.manager.gameInitData;
        Vector3 movePosition = Vector3.zero;
        switch (direction)
        {
            case DirectionEnum.Up:
                movePosition = Vector3.up * gameInit.sizeForCubeMoveStop;
                break;
            case DirectionEnum.Down:
                movePosition = Vector3.down * gameInit.sizeForCubeMoveStop;
                break;
            case DirectionEnum.Left:
                movePosition = Vector3.left * gameInit.sizeForCubeMoveStop;
                break;
            case DirectionEnum.Right:
                movePosition = Vector3.right * gameInit.sizeForCubeMoveStop;
                break;
            case DirectionEnum.Forward:
                movePosition = Vector3.forward * gameInit.sizeForCubeMoveStop;
                break;
            case DirectionEnum.Back:
                movePosition = Vector3.back * gameInit.sizeForCubeMoveStop;
                break;
        }
        cube.transform.DOKill();
        cube.transform.localPosition = cube.cubeData.positionForReal;
        cube.transform.DOLocalMove(cube.cubeData.positionForReal + movePosition, gameInit.timeForCubeMoveStop).SetLoops(2, LoopType.Yoyo).SetDelay(timeForDelay);
    }
}