using System.Collections;
using UnityEngine;

public class AutoDisableFX : MonoBehaviour
{
    public float lifetime = 1f; // durée avant désactivation

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        // Si l'objet est utilisé via un pool ? on désactive
        gameObject.SetActive(false);

        // Si un jour tu fais un pool pour FX, remplace par un ReturnFX(gameObject)
    }
}
