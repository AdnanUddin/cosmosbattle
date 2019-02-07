using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : ControllerBase
{
    [SerializeField]
    private List<GameObject> planetTypeList;

    public LevelOptions Options;

    private IList<PlanetController> planetControllerList;

    private void Initialize()
    {
        this.planetTypeList = new List<GameObject>(this.Options.Levels);
        this.planetControllerList = new List<PlanetController>();
    }

    private void InitializePlanets()
    {
        //this.Initialize();
        this.Options = this.Options ?? new LevelOptions { Levels = 3, PlanetCount = 7 };
        this.planetControllerList = this.planetControllerList ?? new List<PlanetController>();

        int planetCount = this.Options.PlanetCount;
        var planetTypeList = this.planetTypeList.Where(pt => pt != null).ToList();


        var bounds = Camera.main.GetCameraBounds();
        var startPostionCenter = new Vector3(bounds.max.x, 0.0f, 0.0f);
        var left = new Vector3(bounds.min.x, 0, 0);
        var planetGroup  = new PlanetGroup(left, scale: 2.0f);

        foreach (var startPostion in planetGroup.coordinates)
        {
            int randomPlanetTypeIndex = UnityEngine.Random.Range(0, planetTypeList.Count - 1);
            var planetType = planetTypeList[randomPlanetTypeIndex];
            if (planetType != null)
            {
                var planet = PlanetFactory.CreatePlanet(planetTypeList[randomPlanetTypeIndex], startPostion, setActive: true);
                this.planetControllerList.Add(planet);
            }
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

    private void DestroyPlanet(PlanetController planetController)
    {
        var planet = this.planetControllerList.FirstOrDefault(p => p == planetController);
        planet?.DestroyObject();
        if (planetController != null)
        {
            this.planetControllerList.Remove(planetController);
        }
    }

    public override void DestroyObject()
    {
        foreach (var planet in this.planetControllerList)
        {
            planet?.DestroyObject();
        }

        this.planetControllerList.Clear();
    }

    private void SetPlanetActive(PlanetController planetController, bool value)
    {
        var planet = this.planetControllerList.FirstOrDefault(p => p == planetController);
        planet?.SetGameObjectActive(true);
    }
}
