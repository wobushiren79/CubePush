using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneEditorWindow : EditorWindow
{
    public long findSceneId = 0;

    public SceneInfoService sceneInfoService;
    [MenuItem("工具/关卡编辑")]
    static void CreateWindows()
    {
        EditorWindow.GetWindow(typeof(SceneEditorWindow));
    }

    private void OnEnable()
    {
        sceneInfoService = new SceneInfoService();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        EditorUI.GUIText("关卡ID：");
        findSceneId = EditorUI.GUIEditorText(findSceneId);
        if (EditorUI.GUIButton("加载关卡"))
        {
            SceneInfoBean sceneInfo = sceneInfoService.QueryDataById(findSceneId);
            if (sceneInfo == null)
            {
                LogUtil.LogError("没有查询到ID为：" + findSceneId + "的场景数据");
                return;
            }
            CubeHandler.Instance.manager.InitData();
            CubeHandler.Instance.CreateCube(sceneInfo,true);
        }
        GUILayout.EndHorizontal();

        if (EditorUI.GUIButton("获取当前场景数据"))
        {
            CubeHandler.Instance.manager.InitData();
            Transform containerForCube = CubeHandler.Instance.manager.containerForCube;
            Cube[] arrayCubes = containerForCube.GetComponentsInChildren<Cube>();
            SceneInfoDetails sceneInfo = new SceneInfoDetails();
            sceneInfo.listData = new List<SceneInfoItemDetails>();
            for (int i = 0; i < arrayCubes.Length; i++)
            {
                Cube cube = arrayCubes[i];
                SceneInfoItemDetails sceneInfoItem = new SceneInfoItemDetails();
                sceneInfoItem.direction = (int)cube.cubeData.direction;
                sceneInfoItem.position = new Vector3Bean(cube.transform.position);
                sceneInfo.listData.Add(sceneInfoItem);
            }
            LogUtil.Log("Json:" + SceneInfoDetails.GetJson(sceneInfo));
        }

        GUILayout.EndVertical();
    }
}