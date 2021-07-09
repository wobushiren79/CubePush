using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameHandler, GameManager>
{
    public void ChangeGameState(GameStateEnum gameState)
    {
        switch (gameState)
        {
            case GameStateEnum.Pre:
                //初始化角度
                CubeHandler.Instance.manager.containerForCube.eulerAngles = manager.gameInitData.angleForInitContainerAngle;
                CubeHandler.Instance.CreateRandomCube(3, 3, 3);
                CubeHandler.Instance.AnimForCubeInit(()=> { ChangeGameState(GameStateEnum.Gaming); });
                break;
            case GameStateEnum.Gaming:

                break;
            case GameStateEnum.End:
                break;
        }
        manager.gameState = gameState;
    }
}