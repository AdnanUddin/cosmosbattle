using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField]
    private float linearSpeed = 1f;

    [SerializeField]
    private float angularSpeed = 1f;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            this.FollowTarget();
        }
    }

    void FollowTarget()
    {
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.up, target.transform.position - transform.position), angularSpeed * Time.deltaTime);
        this.transform.Translate(Vector3.up * linearSpeed * Time.deltaTime, Space.Self);
    }
}
