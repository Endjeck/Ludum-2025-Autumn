using System.Collections.Generic;
using UnityEngine;

public class DisksManager : MonoBehaviour
{
    [SerializeField] private DiskObject[] _diskObjects;
    [SerializeField] private int[] _sequence;
    [SerializeField] private List<GameObject> _slots;
    [SerializeField] private Animator _doorAnimator;
    private int _emptySlots = 2;
    private int _collectedDisks;
    public void CheckDisks()
    {
        if (_emptySlots !=0) return;
        bool allDone = true;
        for (int i = 0; i < _diskObjects.Length; i++)
        {
            if (_diskObjects[i].AngleState != _sequence[i])
                allDone = false;
        }
        if (allDone)
            OpenDoor();
    }

    public void PlaceDisk(int place)
    {
        _emptySlots--;
            _diskObjects[place].Disk.gameObject.SetActive(true);
            _collectedDisks--;
            _slots.RemoveAt(0);
    }
    public void CollectDisk()
    {
        _collectedDisks++;
        if(_collectedDisks-1<_slots.Count)
            _slots[_collectedDisks-1].tag = "Interactive";
    }
    public void OpenDoor()
    {
        for (int i = 0; i < _diskObjects.Length; i++)
        {
            _diskObjects[i].transform.tag = "Untagged";
        }
        _doorAnimator.SetTrigger("Open");
    }
}
