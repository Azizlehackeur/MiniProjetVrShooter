using UnityEngine;

public class Target : MonoBehaviour
{
    public float destroyDelay = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // FX d'impact
            Instantiate(
                FXLoader.Instance.GetImpactFX(),
                transform.position,
                Quaternion.identity
            );

            // Score
            GameplayManager.Instance.AddScore(10);

            // Désactivation différée
            Invoke(nameof(DisableTarget), destroyDelay);
        }
    }

    void DisableTarget()
    {
        // On retire la cible du compteur
        GameplayManager.Instance.RegisterTargetDespawn();

        // Retour au pool
        TargetPool.Instance.ReturnTarget(gameObject);
    }
}
