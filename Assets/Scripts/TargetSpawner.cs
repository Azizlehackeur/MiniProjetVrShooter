using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // Préfabriqué de la cible
    public float spawnInterval = 8f; // Temps entre les apparitions de cibles
    public float spawnRange = 10f; // Plage de spawn aléatoire

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0f, spawnInterval);
    }

    void SpawnTarget()
    {
        // Générer une position aléatoire pour la cible
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            1f, // Hauteur de spawn
            Random.Range(-spawnRange, spawnRange)
        );

        Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
    }
}