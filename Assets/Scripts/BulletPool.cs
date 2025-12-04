using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [Header("Réglages du pool")]
    [SerializeField] private GameObject bulletPrefab; // même prefab que dans GunShooting
    [SerializeField] private int initialSize = 20;    // taille de départ du pool

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        // Singleton très simple pour un gestionnaire unique
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // On peut garder cet objet entre les scènes si tu veux
        // DontDestroyOnLoad(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewBullet();
        }
    }

    private GameObject CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false); // IMPORTANT : inactive dans le pool au départ
        pool.Add(bullet);
        return bullet;
    }

    /// <summary>
    /// Retourne une balle prête à l’emploi.
    /// On réutilise une inactive, sinon on en crée une nouvelle.
    /// </summary>
    public GameObject GetBullet()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        return CreateNewBullet();
    }

    /// <summary>
    /// Optionnel : fonction appelée par le projectile quand il "meurt"
    /// </summary>
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
