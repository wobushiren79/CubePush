using UnityEditor;
using UnityEngine;

public class Cube : BaseMonoBehaviour
{
    public CubeBean cubeData;

    public void SetData(CubeBean cubeData)
    {
        this.cubeData = cubeData;
        SetPosition();
        SetDirection();
    }

    public void SetPosition()
    {
        transform.localPosition = cubeData.positionForReal;
    }

    public void SetDirection()
    {
        Vector3 angles = GetDirectionAngle();
        transform.localEulerAngles = angles;
    }

    public Vector3 GetDirectionAngle()
    {
        Vector3 angles = Vector3.zero;
        switch (cubeData.direction)
        {
            case DirectionEnum.Up:
                angles = Vector3.zero;
                break;
            case DirectionEnum.Down:
                angles = new Vector3(180, 0, 0);
                break;
            case DirectionEnum.Left:
                angles = new Vector3(0, 0, 90);
                break;
            case DirectionEnum.Right:
                angles = new Vector3(0, 0, -90);
                break;
            case DirectionEnum.Forward:
                angles = new Vector3(-90, 0, 0);
                break;
            case DirectionEnum.Back:
                angles = new Vector3(90, 0, 0);
                break;
        }
        return angles;
    }
}