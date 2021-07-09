using UnityEditor;
using UnityEngine;

public class Launcher : BaseMonoBehaviour
{
    private void Start()
    {
        GameHandler.Instance.ChangeGameState(GameStateEnum.Pre);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("重置"))
        {
            GameHandler.Instance.ChangeGameState(GameStateEnum.Pre);
        }
    }
}