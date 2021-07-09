/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-07-09-17:16:07 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class SceneInfoService : BaseDataRead<SceneInfoBean>
{
    protected readonly string saveFileName;

    public SceneInfoService()
    {
        saveFileName = "SceneInfo";
    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<SceneInfoBean> QueryAllData()
    {
        return BaseLoadDataForList(saveFileName); 
    }
        
    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SceneInfoBean QueryDataById(long id)
    {
        List<SceneInfoBean> listData= QueryAllData();
        for (int i=0;i<listData.Count;i++)
        {
            SceneInfoBean sceneInfo= listData[i];
            if (sceneInfo.id == id)
            {
                return sceneInfo;
            }
        }
        return null;
    }

        /// <summary>
    /// 查询游戏配置数据
    /// </summary>
    /// <returns></returns>
    public SceneInfoBean QueryData()
    {
        return null;
    }
        

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    public void UpdateData(SceneInfoBean data)
    {

    }
}