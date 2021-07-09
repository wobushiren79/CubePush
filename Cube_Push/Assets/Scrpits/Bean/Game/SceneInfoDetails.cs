using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SceneInfoDetails
{
    public List<SceneInfoItemDetails> listData;


    public static string GetJson(SceneInfoDetails sceneInfo)
    {
       return JsonUtil.ToJson(sceneInfo);
    }

    public static SceneInfoDetails SetJson(string data)
    {
        return  JsonUtil.FromJson<SceneInfoDetails>(data);
    }
}