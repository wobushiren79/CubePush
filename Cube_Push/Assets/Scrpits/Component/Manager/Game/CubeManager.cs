using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : BaseManager
{
    public List<Cube> listCube = new List<Cube>();

    public void AddCube(Cube cube)
    {
        listCube.Add(cube);
    }

    public void RemoveCube(Cube cube)
    {
        listCube.Remove(cube);
    }

    public void ClearAllCube()
    {
        for (int i = 0; i < listCube.Count; i++)
        {
            Destroy(listCube[i]);
        }
        listCube.Clear();
    }

    public void GetListCubeByDirection(Cube cube)
    {
        switch (cube.cubeData.direction)
        {
            case DirectionEnum.Up:
                break;
            case DirectionEnum.Down:
                break;
            case DirectionEnum.Left:
                break;
            case DirectionEnum.Right:
                break;
            case DirectionEnum.Forward:
                break;
            case DirectionEnum.Back:
                break;
        }
        for (int i = 0; i < listCube.Count; i++)
        {
            Cube itemCube = listCube[i];

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


}
