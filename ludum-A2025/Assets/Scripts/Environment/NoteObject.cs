using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private GameObject _noteObject;
    public void ActivateNote(bool active)
    {
        _noteObject.SetActive(active);
        if (!active)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}