using UnityEngine;
using System.Collections;
public class ButterflyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveRange = 2;
    //var target:Vector3;
    private Vector3 target;
    //var timer:float;
    private float timer;
    //var sec:int;
    private int sec;

    private Vector3 origin, newTarget, origTarget;

    void Start()
    {
        target = ResetTarget();
        sec = ResetSec();
        origin = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > sec)
        {
            target = ResetTarget();
            origTarget = target;
            sec = ResetSec();
        }
        target = Vector3.Lerp(origTarget, newTarget, timer / sec);
        transform.Translate(target * moveSpeed * Time.deltaTime);
    }

    Vector3 ResetTarget()
    {
        Vector3 tar = new Vector3(Random.Range(origin.x - moveRange, origin.x + moveRange), Random.Range(origin.y - moveRange, origin.y + moveRange), origin.z);
        tar = (tar - transform.position).normalized * Random.Range(0.5f, 2);
        return tar;
    }

    int ResetSec()
    {
        timer = 0;
        return Random.Range(1, 3);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, origin);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + target);
    }
}
