using UnityEditor;
using UnityEngine;

public class PlayerControl : BaseMonoBehaviour
{

    private void Update()
    {
        if (GameHandler.Instance.manager.gameState== GameStateEnum.Gaming)
        {
            HandleForSelect();
            HandleForRotate();
        }
    }

    /// <summary>
    /// 方块选择处理
    /// </summary>
    public void HandleForSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayUtil.RayToScreenPoint(float.MaxValue, 1 << LayerInfo.Cube, out bool isCollider, out RaycastHit hit);
            if (isCollider)
            {
                Cube cube = hit.collider.gameObject.GetComponent<Cube>();
                CubeHandler.Instance.MoveCube(cube);
            }
        }
    }

    /// <summary>
    /// 方块旋转处理
    /// </summary>
    public void HandleForRotate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Transform transform = CubeHandler.Instance.manager.containerForCube;
        GameInitBean gameInit=  GameHandler.Instance.manager.gameInitData;
        transform.Rotate(new Vector3(moveV * Time.deltaTime * gameInit.speedForRotate, moveH * Time.deltaTime * gameInit.speedForRotate, 0), Space.World);
    }
}