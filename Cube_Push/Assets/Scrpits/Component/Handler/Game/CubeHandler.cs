using UnityEditor;
using UnityEngine;

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


    public void MoveCube(Cube cube)
    {
        //首先获取方向上的所有方块


    }

}