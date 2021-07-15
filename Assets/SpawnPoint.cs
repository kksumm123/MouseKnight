using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum SpawnType
    {
        Player,
        Goblin,
        Skeleton,
        Boss,
    }
    [SerializeField] SpawnType spawnType;
    private void Awake()
    {
        string spawnPrefabName;
        switch (spawnType)
        {
            case SpawnType.Player:
                spawnPrefabName = "Player";
                break;
            case SpawnType.Goblin:
                spawnPrefabName = "Goblin";
                break;
            case SpawnType.Skeleton:
                spawnPrefabName = "Skelelton";
                break;
            case SpawnType.Boss:
                spawnPrefabName = "Boss";
                break;
            default:
                spawnPrefabName = "NONE";
                break;
        }
        Instantiate(Resources.Load(spawnPrefabName)
            , transform.position, Quaternion.identity);
    }
}
