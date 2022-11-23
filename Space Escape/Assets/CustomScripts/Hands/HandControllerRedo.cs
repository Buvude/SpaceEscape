using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;


public class HandControllerRedo : MonoBehaviour
{
    public float radius = .1f;
    public LayerMask CollisionLayersLocomotion;
    public InputActionManager inputManager;
    public GameObject VRPlayerOrigin;
    private Vector3 surfactGripTarget;
    private bool grabbingL = false, grabbingR = false;
    public Transform controllerTargetL, controllerTargetR;
    public Vector3 followOffset;
    public float gripMoveLerpRate;
    public HandDismissal hD;


    // Start is called before the first frame update
    void Start()
    {
        /*surfactGripTarget *= -1;*/
        inputManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").actionTriggered += OnInputR;
        inputManager.actionAssets[0].FindActionMap("XRI RightHand Interaction").actionTriggered += OnInputL; //This makes it so both left and right hand are calling the same function... may need to change later [fixxed]
        /* for(int i = 0; i<7; i++)
         {
             Debug.Log(inputManager.actionAssets[0].actionMaps[i].ToString()); to figure out how to activate the right hand
         }*/
    }
    private void OnDisable()
    {
        inputManager.actionAssets[0].actionMaps[2].actionTriggered -= OnInputR;
        inputManager.actionAssets[0].actionMaps[5].actionTriggered -= OnInputL;
    }

    // Update is called once per frame
    void Update()
    {

        //Get actual target position to seek (ALWAYS home toward this, not controllerTarget)
        if (grabbingL||grabbingR)
        {
            Vector3 targetOriginPos = VRPlayerOrigin.transform.position;
            targetOriginPos += surfactGripTarget - transform.position;

            /*targetOriginPos = Vector3.Lerp(currentOriginPos, targetOriginPos, gripMoveLerpRate * Time.deltaTime);*/


            //Cleanup
            /*lastOriginVelocity = targetOriginPos - currentOriginPos;*/   //Record velocity
            VRPlayerOrigin.transform.position = targetOriginPos; //Apply positional change

        }


    }
    public void SwitchHands()
    {
        grabbingL = false;
    }
    public void OnInputL(InputAction.CallbackContext context)
    {

        if (context.action.name == "Grab")
        {
            Debug.Log(context.action.ToString());
            OnGrab(context, false);
        }
    }
    public void OnInputR(InputAction.CallbackContext context)
    {

        if (context.action.name == "Grab")
        {
            Debug.Log(context.action.ToString());
            OnGrab(context, true);
        }


    }
    public void OnGrab(InputAction.CallbackContext context, bool Right)
    {
        if (context.performed)
        {
            if (Right && Physics.CheckSphere(transform.position, radius, CollisionLayersLocomotion))
            {
                Debug.Log("Grabbed");
                /*StartCoroutine("LeftHandLocomotion");*/
                surfactGripTarget = transform.position;
                //TODO: add something so you can hold something in one hand while holding onto a locomotion 
                if (Right)
                {
                    grabbingR = true;
                    grabbingL = false;
                }
                else
                {
                    grabbingL = true;
                    grabbingR = false;
                }
            }
            if (context.canceled)
            {
                /* if (Right) grabbingR = false;
                 if (!Right) grabbingL = false;*/ //should make it so hands will now let go
                grabbingL = false;
                grabbingR = false;
            }
        }

        /*IEnumerator LeftHandLocomotion()
        {
            yield return 0;
        }*/
    }
}

