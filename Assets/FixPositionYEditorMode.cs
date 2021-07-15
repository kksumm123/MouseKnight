using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FixPositionYEditorMode : MonoBehaviour
{
    //Spawn 해주고 존재할 이유가 없으니 파괴
    void Start()
    {
        if (Application.isPlaying)
            Destroy(gameObject);
    }

    void Update()
    {
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }

    [SerializeField] SpawnType spawnType;
    private void OnDrawGizmos()
    {
        string iconName;
        switch (spawnType)
        {
            case SpawnType.Player:
                iconName = "Player";
                break;
            case SpawnType.Goblin:
                iconName = "Goblin";
                break;
            case SpawnType.Skeleton:
                iconName = "Skelelton";
                break;
            case SpawnType.Boss:
                iconName = "Boss";
                break;
            default:
                iconName = "";
                break;
        }
        var f = iconName + ".png";
        Gizmos.DrawIcon(transform.position, f, true);
    }
}
