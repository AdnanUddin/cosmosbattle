using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : ControllerBase
{
    [SerializeField]
    private List<GameObject> planetTypeList;

    private IDictionary<PlanetController, GameObject> planetControllerDictionary;
    private LevelOptions options;

    public LevelController(LevelOptions options)
    {   
        this.options = options;
        this.Initialize();
    }   

    private void Initialize()
    {
        this.planetTypeList = new List<GameObject>(this.options.Levels);
        this.planetControllerDictionary = new Dictionary<PlanetController, GameObject>();
    }

    private void InitializePlanets()
    {
        this.options = this.options ?? new LevelOptions { Levels = 3, PlanetCount = 5 };
        this.planetControllerDictionary = this.planetControllerDictionary ?? new Dictionary<PlanetController, GameObject>();

        int index = 2;
        int planetCount = this.options.PlanetCount;

        foreach(var planetType in this.planetTypeList)
        {
            var startPostion = new Vector3
            {
                x = UnityEngine.Random.Range(-index, index),
                y = UnityEngine.Random.Range(-index, index),
                z = PlanetController.ZAxis
            };

            var planet = Instantiate(planetType, startPostion, Quaternion.identity);
            this.planetControllerDictionary.Add(planet.GetComponent<PlanetController>(), planet);

            Destroy(planet, 10.0f);

            index ++;
        }
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
