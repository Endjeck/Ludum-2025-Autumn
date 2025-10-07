using UnityEngine;

public class ParticleSystemActivator : MonoBehaviour
{
    [Tooltip("������ �� ������� ������, ������� ����� ������������")]
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
                Debug.LogWarning("ParticleSystemToActivate �� ��������� � ����������.");
            }
        }
    }
}