using UnityEngine;

public class PickableDisk : MonoBehaviour
{
    [SerializeField] private DisksManager _disksManager;
    [SerializeField] private ParticleSystem _particles;
    public void CollectDisk()
    {
        _disksManager.CollectDisk();
        _particles.Play();
        gameObject.SetActive(false);
    }
}