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

    public static PlanetGroup CreatePlanetGroup()
    {
        return new PlanetGroup(Vector3.zero, scale: 1.0f);
    }

    public static IEnumerable<PlanetGroup> CreatePlanetGroups(int planetCount)
    {
        var planetGroupList = new List<PlanetGroup>();
        var planetGroupCount = planetCount / 3;
        for (int i = 0; i < planetGroupCount; i = i++)
        {
            planetGroupList.Add(PlanetFactory.CreatePlanetGroup());
        }

        if (planetCount % 3 > 0)
        {
            planetGroupList.Add(PlanetFactory.CreatePlanetGroup());
        }

        return planetGroupList;
    }
}