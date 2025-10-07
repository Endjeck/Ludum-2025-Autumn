using UnityEngine;

public class ParticleSystemActivator : MonoBehaviour
{
    [Tooltip("Ссылка на систему частиц, которую нужно активировать")]
    public ParticleSystem particleSystemToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (particleSystemToActivate != null)
            {
                particleSystemToActivate.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("ParticleSystemToActivate не назначена в инспекторе.");
            }
        }
    }
}