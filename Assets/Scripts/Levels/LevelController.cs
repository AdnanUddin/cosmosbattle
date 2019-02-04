using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : ControllerBase
{
    [SerializeField]
    private List<GameObject> planetTypeList;
    private LevelOptions options;

    private IList<PlanetController> planetControllerList;

    public LevelController(LevelOptions options)
    {   
        this.options = options;
        this.Initialize();
    }   

    private void Initialize()
    {
        this.planetTypeList = new List<GameObject>(this.options.Levels);
        this.planetControllerList = new List<PlanetController>();
    }

    private void InitializePlanets()
    {
        this.options = this.options ?? new LevelOptions { Levels = 3, PlanetCount = 5 };
        this.planetControllerList = this.planetControllerList ?? new List<PlanetController>();

        int index = 2;
        int planetCount = this.options.PlanetCount;
        var planetTypeList = this.planetTypeList.Where(pt => pt != null).ToList();

        for (int i = 0; i < this.options.PlanetCount; i++)
        {     
            int randomPlanetType = UnityEngine.Random.Range(0, planetTypeList.Count - 1);
            var startPostion = new Vector3
            {
                x = UnityEngine.Random.Range(-index, index),
                y = UnityEngine.Random.Range(-index, index),
                z = PlanetController.ZAxis
            };

            var planetType = planetTypeList[randomPlanetType];
            if (planetType != null)
            {
                var planet = Instantiate(planetTypeList[randomPlanetType], startPostion, Quaternion.identity);
                var planetController = planet.GetComponent<PlanetController>();
                planetController.SetGameObjectActive(false);
                this.planetControllerList.Add(planetController);
            }
            index ++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.frameCount > 60 && Time.frameCount % 60 == 0 && this.planetControllerList.Count > 0)
        {
            if (this.planetControllerList.FirstOrDefault(p => !p.IsGameObjectActive) is PlanetController planetController)
            {
                planetController.SetGameObjectActive(true);
            }
        }
    }

    public override void Create()
    {
        this.InitializePlanets();
    }

    private void DestroyPlanet(PlanetController planetController)
    {
        var planet = this.planetControllerList.FirstOrDefault(p => p == planetController);
        planet?.DestroyObject();
        this.planetControllerList.Remove(planetController);
    }

    public override void DestroyObject()
    {
        foreach (var planet in this.planetControllerList)
        {
            planet?.DestroyObject();
        }

        this.planetControllerList.Clear();
    }

    private void SetActive(PlanetController planetController, bool value)
    {
        var planet = this.planetControllerList.FirstOrDefault(p => p == planetController);
        planet?.SetGameObjectActive(true);
    }
}
