using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : BaseManager, ISceneInfoView
{
    public List<Cube> listCube = new List<Cube>();
    public Dictionary<int, SceneInfoBean> dicSceneInfo = new Dictionary<int, SceneInfoBean>();

    public SceneInfoController sceneInfoController;
    private void Awake()
    {
        InitData();
    }

    public void InitData()
    {
        sceneInfoController = new SceneInfoController(this, this);
        sceneInfoController.GetAllSceneInfoData(InitSceneInfoData);
    }

    public void InitSceneInfoData(List<SceneInfoBean> listData)
    {
        dicSceneInfo.Clear();
        for (int i = 0; i < listData.Count; i++)
        {
            SceneInfoBean sceneInfo = listData[i];
            dicSceneInfo.Add(sceneInfo.level, sceneInfo);
        }
    }

    public void AddCube(Cube cube)
    {
        listCube.Add(cube);
    }

    public void RemoveCube(Cube cube)
    {
        listCube.Remove(cube);
        cube.transform.SetParent(null);
    }

    public void ClearAllCube()
    {
        for (int i = 0; i < listCube.Count; i++)
        {
            Destroy(listCube[i].gameObject);
        }
        listCube.Clear();
    }

    /// <summary>
    /// 检测四周是否有方块
    /// </summary>
    /// <param name="cube"></param>
    /// <returns></returns>
    public bool CheckRangeCube(Cube cube)
    {
        switch (cube.cubeData.direction)
        {
            case DirectionEnum.Up:
                return CheckRangeCubeByDirection(cube, new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1), new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0));
            case DirectionEnum.Down:
                return CheckRangeCubeByDirection(cube, new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1), new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0));
            case DirectionEnum.Left:
                return CheckRangeCubeByDirection(cube, new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0));
            case DirectionEnum.Right:
                return CheckRangeCubeByDirection(cube, new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0));
            case DirectionEnum.Forward:
                return CheckRangeCubeByDirection(cube, new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0));
            case DirectionEnum.Back:
                return CheckRangeCubeByDirection(cube, new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0));
        }
        return false;
    }

    public bool CheckRangeCubeByDirection(Cube cube, Vector3Int up, Vector3Int down, Vector3Int left, Vector3Int right)
    {
        int number = 0;
        for (int i = 0; i < listCube.Count; i++)
        {
            Cube itemCube = listCube[i];
            if (itemCube.cubeData.positionForMark == cube.cubeData.positionForMark + up
                || itemCube.cubeData.positionForMark == cube.cubeData.positionForMark + down
                || itemCube.cubeData.positionForMark == cube.cubeData.positionForMark + left
                || itemCube.cubeData.positionForMark == cube.cubeData.positionForMark + right)
            {
                number++;
            }
        }
        return number == 4 ? true : false;
    }

    public Cube GetCubeByMarkPosition(Vector3Int markPosition)
    {
        for (int i = 0; i < listCube.Count; i++)
        {
            Cube itemCube = listCube[i];
            if (markPosition == itemCube.cubeData.positionForMark)
            {
                return itemCube;
            }
        }
        return null;
    }

    public void GetListCubeByDirection(Cube cube, out List<Cube> listData, out Vector3Int closePosition)
    {
        listData = new List<Cube>();
        closePosition = Vector3Int.zero;
        float tempDis = float.MaxValue;
        for (int i = 0; i < listCube.Count; i++)
        {
            Cube itemCube = listCube[i];
            if (cube == itemCube)
                continue;
            bool addCube = false;
            switch (cube.cubeData.direction)
            {
                case DirectionEnum.Up:
                    if (itemCube.cubeData.positionForMark.x == cube.cubeData.positionForMark.x
                        && itemCube.cubeData.positionForMark.y > cube.cubeData.positionForMark.y
                        && itemCube.cubeData.positionForMark.z == cube.cubeData.positionForMark.z)
                    {
                        addCube = true;
                        listData.Add(itemCube);
                    }
                    break;
                case DirectionEnum.Down:
                    if (itemCube.cubeData.positionForMark.x == cube.cubeData.positionForMark.x
                        && itemCube.cubeData.positionForMark.y < cube.cubeData.positionForMark.y
                        && itemCube.cubeData.positionForMark.z == cube.cubeData.positionForMark.z)
                    {
                        addCube = true;
                        listData.Add(itemCube);
                    }
                    break;
                case DirectionEnum.Left:
                    if (itemCube.cubeData.positionForMark.x < cube.cubeData.positionForMark.x
                        && itemCube.cubeData.positionForMark.y == cube.cubeData.positionForMark.y
                        && itemCube.cubeData.positionForMark.z == cube.cubeData.positionForMark.z)
                    {
                        addCube = true;
                        listData.Add(itemCube);
                    }
                    break;
                case DirectionEnum.Right:
                    if (itemCube.cubeData.positionForMark.x > cube.cubeData.positionForMark.x
                   && itemCube.cubeData.positionForMark.y == cube.cubeData.positionForMark.y
                   && itemCube.cubeData.positionForMark.z == cube.cubeData.positionForMark.z)
                    {
                        addCube = true;
                        listData.Add(itemCube);
                    }
                    break;
                case DirectionEnum.Forward:
                    if (itemCube.cubeData.positionForMark.x == cube.cubeData.positionForMark.x
                        && itemCube.cubeData.positionForMark.y == cube.cubeData.positionForMark.y
                        && itemCube.cubeData.positionForMark.z < cube.cubeData.positionForMark.z)
                    {
                        addCube = true;
                        listData.Add(itemCube);
                    }
                    break;
                case DirectionEnum.Back:
                    if (itemCube.cubeData.positionForMark.x == cube.cubeData.positionForMark.x
                        && itemCube.cubeData.positionForMark.y == cube.cubeData.positionForMark.y
                        && itemCube.cubeData.positionForMark.z > cube.cubeData.positionForMark.z)
                    {
                        addCube = true;
                        listData.Add(itemCube);
                    }
                    break;
            }
            if (addCube)
            {
                //获取最近的方块
                float dis = Vector3.Distance(itemCube.cubeData.positionForReal, cube.cubeData.positionForReal);
                if (dis <= tempDis)
                {
                    switch (cube.cubeData.direction)
                    {
                        case DirectionEnum.Up:
                            closePosition = itemCube.cubeData.positionForMark + new Vector3Int(0, -1, 0);
                            break;
                        case DirectionEnum.Down:
                            closePosition = itemCube.cubeData.positionForMark + new Vector3Int(0, 1, 0);
                            break;
                        case DirectionEnum.Left:
                            closePosition = itemCube.cubeData.positionForMark + new Vector3Int(1, 0, 0);
                            break;
                        case DirectionEnum.Right:
                            closePosition = itemCube.cubeData.positionForMark + new Vector3Int(-1, 0, 0);
                            break;
                        case DirectionEnum.Forward:
                            closePosition = itemCube.cubeData.positionForMark + new Vector3Int(0, 0, 1);
                            break;
                        case DirectionEnum.Back:
                            closePosition = itemCube.cubeData.positionForMark + new Vector3Int(0, 0, -1);
                            break;
                    }
                    tempDis = dis;
                }
            }
        }
    }

    protected Transform _containerForCube;

    protected Transform _modelForCube;

    public Transform containerForCube
    {
        get
        {
            if (_containerForCube == null)
            {
                _containerForCube = FindWithTag<Transform>(TagInfo.Tag_ContainerCube);
            }
            return _containerForCube;
        }
    }


    public Transform modelForCube
    {
        get
        {
            if (_modelForCube == null)
            {
                _modelForCube = LoadResourcesUtil.SyncLoadData<Transform>("Game/ModelForCube");
            }
            return _modelForCube;
        }
    }

    #region 数据回调
    public void GetSceneInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetSceneInfoFail(string failMsg, Action action)
    {

    }
    #endregion
}
