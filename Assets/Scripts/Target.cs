using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject particleEffectPrefab;  // Prefab de l'effet de particules (explosion, fumée, etc.)
    public float destroyDelay = 0.5f;        // Délai avant de détruire la cible (en secondes)

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet entrant est une balle
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Déclencher l'effet de particules à l'endroit où la cible a été touchée
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

            // Détruire la cible avec un délai pour permettre aux particules de se jouer
            Destroy(gameObject, destroyDelay);
        }
    }
}
