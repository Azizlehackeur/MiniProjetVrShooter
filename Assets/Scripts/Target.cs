using UnityEngine;

public class Target : MonoBehaviour
{
    public float destroyDelay = 0.5f; // délai avant de désactiver la cible

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // FX d'impact via Addressables : PCVR ou Quest automatiquement
            Instantiate(
                FXLoader.Instance.GetImpactFX(),
                transform.position,
                Quaternion.identity
            );

            // Désactiver la cible après un délai (pooling)
            Invoke(nameof(DisableTarget), destroyDelay);
        }
    }

    void DisableTarget()
    {
        TargetPool.Instance.ReturnTarget(gameObject);
    }
}
