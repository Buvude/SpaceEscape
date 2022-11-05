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
    public GameObject VRPlayerOrigin;
    private Vector3 surfactGripTarget;
    private bool grabbing = false;
    public Transform controllerTarget;
    public Vector3 followOffset;
    public float gripMoveLerpRate;

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
        Vector3 offsetTargetPos = controllerTarget.position + controllerTarget.TransformVector(followOffset);                              //Get actual target position to seek (ALWAYS home toward this, not controllerTarget)
        if (grabbing)
        {
            Vector3 currentOriginPos = VRPlayerOrigin.transform.position;
            Vector3 targetOriginPos = currentOriginPos;
            targetOriginPos += surfactGripTarget - offsetTargetPos;

            targetOriginPos = Vector3.Lerp(currentOriginPos, targetOriginPos, gripMoveLerpRate * Time.deltaTime);
            
            
            //Cleanup
            /*lastOriginVelocity = targetOriginPos - currentOriginPos;*/   //Record velocity
            VRPlayerOrigin.transform.position = targetOriginPos; //Apply positional change

        }

        
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
                /*StartCoroutine("LeftHandLocomotion");*/
                grabbing = true;
            }
        }
    }

    /*IEnumerator LeftHandLocomotion()
    {
        yield return 0;
    }*/
}
