using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class WrongDoor : MonoBehaviour
{
    public void OpenDoor()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}