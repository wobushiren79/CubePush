using UnityEditor;
using UnityEngine;

public class GameManager :  BaseManager
{
    public GameStateEnum gameState = GameStateEnum.Pre;


    protected GameInitBean _gameInitData;
    public GameInitBean gameInitData
    {
        get
        {
            if (_gameInitData==null)
            {
                _gameInitData = LoadResourcesUtil.SyncLoadData<GameInitBean>("Data/GameInit");
            }
            return _gameInitData;
        }
    }
}