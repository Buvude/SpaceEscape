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
        /*surfactGripTarget *= -1;*/
        inputManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").actionTriggered += OnInput;
        inputManager.actionAssets[0].FindActionMap("XRI RightHand Interaction").actionTriggered += OnInput; //This makes it so both left and right hand are calling the same function... may need to change later
       /* for(int i = 0; i<7; i++)
        {
            Debug.Log(inputManager.actionAssets[0].actionMaps[i].ToString()); to figure out how to activate the right hand
        }*/
    }
    private void OnDisable()
    {
        inputManager.actionAssets[0].actionMaps[2].actionTriggered -= OnInput;
        inputManager.actionAssets[0].actionMaps[5].actionTriggered -= OnInput;
    }

    // Update is called once per frame
    void Update()
    {
        
                                   //Get actual target position to seek (ALWAYS home toward this, not controllerTarget)
        if (grabbing)
        {
            Vector3 targetOriginPos = VRPlayerOrigin.transform.position; 
            targetOriginPos += surfactGripTarget-transform.position;

            /*targetOriginPos = Vector3.Lerp(currentOriginPos, targetOriginPos, gripMoveLerpRate * Time.deltaTime);*/


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
                surfactGripTarget = transform.position;
            }
        }
        if (context.canceled)
        {
            grabbing = false; //should make it so hands will now let go
        }
    }

    /*IEnumerator LeftHandLocomotion()
    {
        yield return 0;
    }*/
}
