using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FixPositionYEditorMode : MonoBehaviour
{
    //Spawn ���ְ� ������ ������ ������ �ı�
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
}
