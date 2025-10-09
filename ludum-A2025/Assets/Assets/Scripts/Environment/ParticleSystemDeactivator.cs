using UnityEngine;

public class ParticleSystemDeactivator : MonoBehaviour
{
    [Tooltip("������ �� ������� ������, ������� ����� ������������")]
    public ParticleSystem particleSystemToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (particleSystemToActivate != null)
            {
                particleSystemToActivate.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("ParticleSystemToActivate �� ��������� � ����������.");
            }
        }
    }
}