using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePrefabTest : CollectableBaseItem
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Interracting with player <3");
            base.OnTriggerEnter(other);
        }
    }
}
