using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;


public class HandControllerRedo : MonoBehaviour
{
    public static  List<HandControllerRedo> handList= new List<HandControllerRedo>();
    private HandControllerRedo otherHand;
    public bool rightHand;
    public float radius = .1f;
    public LayerMask CollisionLayersLocomotion;
    public InputActionManager inputManager;
    public GameObject VRPlayerOrigin;
    private Vector3 surfaceGripTarget;
    internal bool grabbing = false;
    public Vector3 followOffset;
    public float gripMoveLerpRate;
    public HandDismissal hD;

    private void Awake()
    {
        handList.Add(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        /*if()*/
        /*surfactGripTarget *= -1;*/
        if (rightHand)
        {
            inputManager.actionAssets[0].FindActionMap("XRI RightHand Interaction").actionTriggered += OnInput;
        } 
        else
        {
            inputManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").actionTriggered += OnInput; //This makes it so both left and right hand are calling the same function... may need to change later [fixxed]
        }

        HandControllerRedo[] hands = transform.parent.GetComponentsInChildren<HandControllerRedo>();
        if (hands[0] == this) { otherHand = hands[1]; } else { otherHand = hands[0]; }

        /* for(int i = 0; i<7; i++)
         {
             Debug.Log(inputManager.actionAssets[0].actionMaps[i].ToString()); to figure out how to activate the right hand
         }*/
    }
    private void OnDisable()
    {
        if (rightHand) inputManager.actionAssets[0].actionMaps[2].actionTriggered -= OnInput;
        else inputManager.actionAssets[0].actionMaps[5].actionTriggered -= OnInput;
    }

    // Update is called once per frame
    void Update()
    {

        //Get actual target position to seek (ALWAYS home toward this, not controllerTarget)
        if (grabbing)
        {
            print(surfaceGripTarget);
            Vector3 targetOriginPos = VRPlayerOrigin.transform.position;
            targetOriginPos += surfaceGripTarget - transform.position;

            /*targetOriginPos = Vector3.Lerp(currentOriginPos, targetOriginPos, gripMoveLerpRate * Time.deltaTime);*/


            //Cleanup
            /*lastOriginVelocity = targetOriginPos - currentOriginPos;*/   //Record velocity
            VRPlayerOrigin.transform.position = targetOriginPos; //Apply positional change

        }


    }
    public void SwitchHands()
    {
        
    }
    public void OnInput(InputAction.CallbackContext context)
    {

        if (context.action.name == "Grab")
        {
            if (context.canceled)
            {
                /* if (Right) grabbingR = false;
                 if (!Right) grabbingL = false;*/ //should make it so hands will now let go
                grabbing = false;
            }
            else if (context.started)
            {
                //Debug.Log(context.action.ToString());
                OnGrab();
            }
        }

    }
    public void OnGrab()
    {
        if (Physics.CheckSphere(transform.position, radius, CollisionLayersLocomotion))
        {
            Debug.Log("Grabbed");
            /*StartCoroutine("LeftHandLocomotion");*/
            surfaceGripTarget = transform.position;
            grabbing = true;
            otherHand.grabbing = false;
            //TODO: add something so you can hold something in one hand while holding onto a locomotion
        }


        /*IEnumerator LeftHandLocomotion()
        {
            yield return 0;
        }*/
    }

}