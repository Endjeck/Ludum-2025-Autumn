using UnityEngine;

public class EmptyDisk : MonoBehaviour
{
    [SerializeField]private DisksManager _disksManager;
    [SerializeField] private int _place;

    public void InsertDisk()
    {
        _disksManager.PlaceDisk(_place);
        gameObject.SetActive(false);
    }
}