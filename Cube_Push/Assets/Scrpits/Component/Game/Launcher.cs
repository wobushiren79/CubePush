using UnityEditor;
using UnityEngine;

public class Launcher : BaseMonoBehaviour
{
    private void Start()
    {
        CubeHandler.Instance.CreateRandomCube(3, 3, 3);
    }
}