using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : MonoBehaviour
{
    public static PlanetController CreatePlanet(GameObject planetType, Vector3 position, bool setActive = false)
    {
        var planet = Instantiate(planetType, position, Quaternion.identity);
        var controller = planetType.GetComponent<PlanetController>();
        controller.SetGameObjectActive(setActive);
        return controller;
    }

    public static IEnumerable<PlanetController> CreatePlanetGroup(int count, GameObject planetType)
    {
        var planetList = new List<PlanetController>();
        for (int i = 0; i < count; i++)
        {
            planetList.Add(CreatePlanet(planetType, Vector3.zero));
        }

        return planetList;
    }
}