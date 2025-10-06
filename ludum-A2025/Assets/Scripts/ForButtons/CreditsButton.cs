using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CreditsCanvas;

    public void OnCreditsButtonClicked()
    {
        if (MainMenuCanvas != null && CreditsCanvas != null)
        {
            MainMenuCanvas.SetActive(false);
            CreditsCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Canvas references are not assigned in CreditsButton script.");
        }
    }
}