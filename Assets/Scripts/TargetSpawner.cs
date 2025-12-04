using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public float spawnInterval = 8f;
    public float spawnRange = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0f, spawnInterval);
    }

    void SpawnTarget()
    {
        // Vérifier que le pool existe
        if (TargetPool.Instance == null)
        {
            Debug.LogError("❌ TargetPool.Instance est NULL ! Assure-toi qu'un TargetPool existe dans la scène.");
            return;
        }

        // Récupérer une cible depuis le pool
        GameObject target = TargetPool.Instance.GetTarget();

        // Vérifier que le pool a bien renvoyé une cible
        if (target == null)
        {
            Debug.LogError("❌ GetTarget() a retourné NULL. Vérifie ton TargetPool ou le prefab assigné.");
            return;
        }

        // Générer une position de spawn
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            1f,
            Random.Range(-spawnRange, spawnRange)
        );

        // Position + rotation
        target.transform.position = spawnPosition;
        target.transform.rotation = Quaternion.identity;

        // Activer la cible
        target.SetActive(true);
    }
}
