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

    public void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.layer+"trigger");
        if (other.gameObject.layer == 9)
        {
            EE.DeathRespawn1();
        }
    }
}
