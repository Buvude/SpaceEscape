using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerers : MonoBehaviour
{
    public EventExecuter EE;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.ToString() == "Death")
        {
            EE.DeathRespawn1();
        }
    }
}
