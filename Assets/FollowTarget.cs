using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Transform target;
    [SerializeField] float originY;
    void Start()
    {
        target = transform.parent;
        originY = transform.position.y;
    }

    void Update()
    {
        var newPos = transform.position;
        newPos.y = originY;

        transform.position = newPos;
    }
}
