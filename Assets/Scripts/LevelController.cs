using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject planet;
    private PlanetController planetController;

    // Start is called before the first frame update
    void Start()
    {
        this.InitializePlanets();
    }

    private void InitializePlanets()
    {
        Instantiate(planet, Vector3.zero, Quaternion.identity);

        this.planetController = planet.GetComponent<PlanetController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
