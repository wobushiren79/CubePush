using UnityEditor;
using UnityEngine;

public class PlayerControl : BaseMonoBehaviour
{
    private void Update()
    {
        HandleForSelect();
    }

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
}