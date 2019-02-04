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
        transform.Translate(Vector3.forward *  Time.deltaTime * linearSpeed);

        var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * angularSpeed);
        // this.transform.Translate(Vector3.up * linearSpeed * Time.deltaTime, Space.Self);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            col.transform.GetComponent<PlanetController>().SubtractUnit(1);

            GameObject.Instantiate(explosion, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal), this.transform);

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
