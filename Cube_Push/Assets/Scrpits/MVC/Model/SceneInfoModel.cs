/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-07-09-17:16:07 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SceneInfoModel : BaseMVCModel
{
    protected SceneInfoService serviceSceneInfo;

    public override void InitData()
    {
        serviceSceneInfo = new SceneInfoService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<SceneInfoBean> GetAllSceneInfoData()
    {
        List<SceneInfoBean> listData = serviceSceneInfo.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    /// <returns></returns>
    public SceneInfoBean GetSceneInfoData()
    {
        SceneInfoBean data = serviceSceneInfo.QueryData();
        if (data == null)
            data = new SceneInfoBean();
        return data;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SceneInfoBean GetSceneInfoDataById(long id)
    {
        SceneInfoBean data = serviceSceneInfo.QueryDataById(id);
        return data;
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetSceneInfoData(SceneInfoBean data)
    {
        serviceSceneInfo.UpdateData(data);
    }

}