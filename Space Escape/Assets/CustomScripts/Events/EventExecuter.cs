using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExecuter : MonoBehaviour
{
    public GameObject Player;
    public Transform respawn1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathRespawn1()
    {
        Player.transform.position = respawn1.position;
    }
}
