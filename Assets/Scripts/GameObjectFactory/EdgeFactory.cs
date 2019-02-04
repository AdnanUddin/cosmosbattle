using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeFactory
{
    public static EdgeController CreateEdge(GameObject edge, PlanetController firstPlanet, PlanetController secondPlanet)
    {
        var edgeController = edge.GetComponent<EdgeController>();
        edgeController.FirstPlanetController = firstPlanet;
        edgeController.SecondPlanetController = secondPlanet;
        edgeController.Create();
        return edgeController;
    }
    
}