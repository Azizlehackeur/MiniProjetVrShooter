using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WeaponFeedback : MonoBehaviour
{
    XRGrabInteractable grab;
    Renderer rend;
    MaterialPropertyBlock block;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }

    void OnEnable()
    {
        grab.hoverEntered.AddListener(OnHoverEnter);
        grab.hoverExited.AddListener(OnHoverExit);
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.hoverEntered.RemoveListener(OnHoverEnter);
        grab.hoverExited.RemoveListener(OnHoverExit);
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        SetHighlight(0.5f);
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        SetHighlight(0f);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        SetHighlight(1f);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        SetHighlight(0f);
    }

    void SetHighlight(float value)
    {
        rend.GetPropertyBlock(block);
        block.SetFloat("_HighlightIntensity", value);
        rend.SetPropertyBlock(block);
    }
}
