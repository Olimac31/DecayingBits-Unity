using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int spawnID;
    void Start()
    {
        GetComponent<Renderer>().enabled = false; //Make invisible

        if(spawnID == GameManager.instance.targetSpawn)
        {
            GameObject.Find("obj_Player").transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }
}