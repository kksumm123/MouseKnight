using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightArea : MonoBehaviour
{
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;

        var spawnPoints = GetComponentsInChildren<SpawnPoint>();

        foreach (var item in spawnPoints)
        {
            item.gameObject.SetActive(false);
        }
    }
}
