using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField]
    private float linearSpeed = 1f;

    [SerializeField]
    private float angularSpeed = 1f;

    [SerializeField]
    private ParticleSystem explosion;

    public GameObject source;

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
        transform.Translate(Vector3.forward * Time.deltaTime * linearSpeed);

        var rotation = Quaternion.LookRotation((target.transform.position - transform.position) + (Random.insideUnitSphere * 0.7f));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * angularSpeed);
        // this.transform.Translate(Vector3.up * linearSpeed * Time.deltaTime, Space.Self);
    }

    // void OnTriggerExit(Collider other)
    // {
        
    // }

    void OnTriggerEnter(Collider col)
    {
        var planetController = col.transform.GetComponent<PlanetController>();

        if (col.gameObject == target)
        {
            if (col.tag != this.tag)
            {
                planetController.SubtractUnit(1, this.tag);

                GameObject.Instantiate(explosion, this.transform.position,Quaternion.identity, this.transform);
            }
            else
            {
                planetController.AddUnit(1);
            }

            StartCoroutine(DestroyItself());
        }
    }

    IEnumerator DestroyItself()
    {
        target = null;

        GameObject.Destroy(this.transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(3);
        GameObject.Destroy(this.transform.gameObject);
    }
}
