using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneIndex = 2;

    public void sceneLoad()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
