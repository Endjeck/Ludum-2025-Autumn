using UnityEngine;
using TMPro;

public class Rightdoor : MonoBehaviour
{
    public GameObject Door;

    public void OpenDoor()
    {
        Door.SetActive(false);
    }
}

