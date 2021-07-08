using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : BaseMonoBehaviour
{

    public AssetReference assetReference;
    public AssetLabelReference labelReference;
    private void OnGUI()
    {
        if (GUILayout.Button("输出"))
        {

        }
    }

    private void Start()
    {
        //单纯加载
        //Addressables.LoadAssetAsync<GameObject>("Add").Completed += GetData2;
        //Addressables.LoadSceneAsync();
        //Addressables.LoadAssetsAsync<GameObject>(new string[] { "Add", "Dev" }, null).Completed += GetData3;
        //Addressables.LoadAssetsAsync<GameObject>(new List<string>{ "Add", "Dev" },null,Addressables.MergeMode.Intersection).Completed += GetData3;
        //实例化
        //Addressables.InstantiateAsync("Add").Completed += GetData;
        //Addressables.InstantiateAsync("Add").Completed += GetData;
        //Addressables.InstantiateAsync("Add").Completed += GetData;
        //Addressables.InstantiateAsync("Add").Completed += GetData;
    }

    public void GetData(AsyncOperationHandle<GameObject> data)
    {
        data.Result.transform.position = Vector3.zero; LogUtil.Log("成功");
    }

    public void GetData2(AsyncOperationHandle<GameObject> data)
    {
        GameObject obj = Instantiate(data.Result);
        //Addressables.ReleaseInstance(obj);
    }
    public void GetData3(AsyncOperationHandle<IList<GameObject>> data)
    {
        IList<GameObject> listGameObject = data.Result;
        for (int i = 0; i < listGameObject.Count; i++)
        {
            GameObject obj = Instantiate(listGameObject[i]);
        }
        //Addressables.ReleaseInstance(obj);
    }
}
