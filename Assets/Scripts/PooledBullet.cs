using UnityEngine;

public class PooledBullet : MonoBehaviour
{
    public float lifetime = 3f; // Durée de vie avant retour au pool
    private float timer;

    void OnEnable()
    {
        // Reset du compteur à chaque activation
        timer = lifetime;
    }

    void Update()
    {
        // Compte à rebours
        timer -= Time.deltaTime;

        // Quand le temps est écoulé → on retourne la balle dans le pool
        if (timer <= 0f)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Quand la balle touche quelque chose, on la désactive
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}
