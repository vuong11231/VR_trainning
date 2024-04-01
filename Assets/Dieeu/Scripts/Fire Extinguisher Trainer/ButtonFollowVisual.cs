using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEditor.PlayerSettings;

public class ButtonFollowVisual : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable xrBaseInteractable;
    [SerializeField] private Transform visualTarget;
    [SerializeField] Vector3 localAxis;
    [SerializeField] float resetSpeed = 5.0f;
    [SerializeField] float followAngleTreshold = 45f;
    private bool isFollowing = false;
    private bool isFreeze = false;
    private Vector3 offset;
    private Vector3 initialLocalPosition;
    private Transform pokeAttachTransform;
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = visualTarget.localPosition;
        xrBaseInteractable.hoverEntered.AddListener(FollowFinger);
        xrBaseInteractable.hoverExited.AddListener(ResetButton);
        xrBaseInteractable.selectEntered.AddListener(Freeze);
    }

    public void FollowFinger(HoverEnterEventArgs hoverEvent)
    {
        //if (hoverEvent.interactableObject is XRPokeInteractor)
        XRPokeInteractor interactor = hoverEvent.interactorObject as XRPokeInteractor;

        pokeAttachTransform = interactor.attachTransform;
        offset = visualTarget.position - pokeAttachTransform.position;

        float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
        if (pokeAngle < followAngleTreshold)
        {
            isFollowing = true;
            isFreeze = false;
        }
    }

    public void ResetButton(BaseInteractionEventArgs hoverEvent)
    {
        isFollowing = false;
    }

    public void Freeze(BaseInteractionEventArgs hoverEvent)
    {
        isFreeze = true;
    }

    private void Update()
    {
        if (isFreeze)
            return;

        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            visualTarget.position = visualTarget.TransformPoint(constainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPosition, resetSpeed * Time.deltaTime);
        }
    }
}
