using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : ControllerBase
{
    [SerializeField]
    private List<GameObject> planetTypeList;
    private IDictionary<PlanetController, GameObject> planetController;


    public LevelController(int levels)
    {
        this.Initialize(levels);
    }   

    private void Initialize(int levels)
    {
        this.planetTypeList = new List<GameObject>(levels);
        this.planetController = new Dictionary<PlanetController, GameObject>();
    }

    private void InitializePlanets()
    {
        int index = 0;
        foreach(var p in this.planetTypeList)
        {
            var startPostion = new Vector3
            {
                x = UnityEngine.Random.Range(0.0f, 20f) * index,
                y = UnityEngine.Random.Range(0.0f, 50f) * index,
                z = PlanetController.ZAxis
            };
            
            Instantiate(p, startPostion, Quaternion.identity);

            this.planetController.Add(p.GetComponent<PlanetController>(), p);
        }

        var planet = new GameObject();
        Instantiate(planet, Vector3.zero, Quaternion.identity);

        this.planetController.Add(planet.GetComponent<PlanetController>(), planet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Create()
    {
        this.InitializePlanets();
    }
}
