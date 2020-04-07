using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag != "Player")
        {
            return;
        }
        Destroy(this.gameObject);
    }
}
