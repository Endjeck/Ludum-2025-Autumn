using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        // Если игра запущена в редакторе, остановим режим игры
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // В билде выйдем из приложения
        Application.Quit();
#endif
    }
}