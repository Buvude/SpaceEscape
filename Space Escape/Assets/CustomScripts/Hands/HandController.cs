using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HandController : MonoBehaviour
{
    public float radius;
    public LayerMask CollisionLayers;
    public InputActionManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").actionTriggered += OnInput;
    }
    private void OnDisable()
    {
        inputManager.actionAssets[0].actionMaps[2].actionTriggered -= OnInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "Grab") OnGrab(context);
    }
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Physics.CheckSphere(transform.position, radius, CollisionLayers))
            {
                Debug.Log("Grabbed");
                StartCoroutine("LeftHandLocomotion");
            }
        }
    }

    IEnumerator LeftHandLocomotion()
    {

        yield return 0;
    }
}
