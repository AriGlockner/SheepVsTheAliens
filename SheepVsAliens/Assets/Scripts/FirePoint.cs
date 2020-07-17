using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    private Transform target;

    public float speed;

    public void chase(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distancePerFrame)
        {
            hitTarget();
            return;
        }
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
    }

    void hitTarget()
    {
        Debug.Log("1, 2, 3, Hits!");
    }
}
