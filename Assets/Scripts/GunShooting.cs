using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Préfabriqué de la balle
    public Transform bulletSpawn;     // Position de spawn de la balle
    public float shootingForce = 1000f; // Force de propulsion de la balle
    public float fireRate = 0.2f;     // Temps entre deux tirs (en secondes)
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Le composant XRGrabInteractable attaché au pistolet
    public ParticleSystem muzzle;   // tir de la bouche de feu

    private XRInputActions inputActions; // Référence aux InputActions générés
    private bool isShooting = false;    // Détecter si le joueur tire
    private float nextFireTime = 0f;    // Temps pour le prochain tir
    private bool isGunHeld = false;     // Détecter si le pistolet est saisi

    void Awake()
    {
        // Initialiser les Input Actions
        inputActions = new XRInputActions();
    }

    void OnEnable()
    {
        // Activer les Input Actions
        inputActions.XRControls.Enable();

        // Lier les événements de tir aux actions de la gâchette
        inputActions.XRControls.Shoot.started += OnShootStart;
        inputActions.XRControls.Shoot.canceled += OnShootStop;

        // Lier les événements de grab et release du pistolet
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        // Désactiver les Input Actions
        inputActions.XRControls.Disable();

        // Désabonner les événements de tir
        inputActions.XRControls.Shoot.started -= OnShootStart;
        inputActions.XRControls.Shoot.canceled -= OnShootStop;

        // Désabonner les événements de grab et release
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        // Vérifier si le pistolet est tenu et si le joueur est en train de tirer
        if (isGunHeld && isShooting && Time.time >= nextFireTime)
        {
            Debug.Log("Tir effectué !");
            Shoot();
            nextFireTime = Time.time + fireRate;
            // Calculer le temps pour le prochain tir
        }
    }


    void OnShootStart(InputAction.CallbackContext context)
    {
        // Le joueur commence à tirer
        isShooting = true;
    }

    void OnShootStop(InputAction.CallbackContext context)
    {
        // Le joueur arrête de tirer
        isShooting = false;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGunHeld = true;
        Debug.Log("Pistolet saisi !");
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGunHeld = false;
        Debug.Log("Pistolet lâché !");
    }


    void Shoot()
    {
        // Créer la balle à la position de spawn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        muzzle.Play(); 

        // Appliquer une force à la balle pour simuler le tir
        rb.AddForce(bulletSpawn.forward * shootingForce);
    }

}