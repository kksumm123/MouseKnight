using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float moveAbleDistance = 3;
    Transform moustPointer;
    void Start()
    {
        moustPointer = GetComponent<Transform>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            moustPointer.position = hit.point;
            float distnace = Vector3.Distance(hit.point, transform.position);
            if (distnace > moveAbleDistance)
            {
                var dir = hit.point - transform.position;
                dir.Normalize();
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }
    }
}
