using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDismissal : MonoBehaviour
{
    // Start is called before the first frame update
    public HandController rightHandController, leftHandController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void newLeftGrab()
    {
        rightHandController.SwitchHands();
    }
    public void newRightGrab()
    {
        leftHandController.SwitchHands();
    }
}
