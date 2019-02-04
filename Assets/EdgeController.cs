using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : ControllerBase
{
    [SerializeField]
    private GameObject firstPlanet;
    
    [SerializeField]
    private GameObject secondPlanet;

    public PlanetController FirstPlanetController;
    public PlanetController SecondPlanetController;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeComponent()
    {
        GameObject firstObject;
        GameObject secondObject;
        
        if (this.FirstPlanetController == null)
        {
            var firstVector = new Vector3(3, 0, 0);
            firstObject = Instantiate(firstPlanet, firstVector, Quaternion.identity);
        }
        else
        {
            firstObject = this.FirstPlanetController.gameObject;
        }

        if (this.SecondPlanetController == null)
        {
            var secondVector = new Vector3(3, 0, 0);
            secondObject = Instantiate(secondPlanet, secondVector, Quaternion.identity);
        }
        else
        {
            secondObject = this.SecondPlanetController.gameObject;
        }
        
        // var secondVector = new Vector3(-5, 0, 0);
        // secondObject = Instantiate(secondPlanet, secondVector, Quaternion.identity);
        
        var firstController = firstObject.GetComponent<PlanetController>();
        var secondController = secondObject.GetComponent<PlanetController>();

        var distance = CalculationHelper.GetDistance(firstController, secondController);
        var rotation = CalculationHelper.GetAngle(firstController, secondController) - 90f; // offset from global space
        var startingPosition = (CalculationHelper.GetDirectionVector(firstController, secondController) / 2) + firstObject.transform.position;
        
        this.gameObject.transform.localScale = new Vector3(0.1f, distance, 0.1f);
        this.gameObject.transform.position = startingPosition;
        this.gameObject.transform.Rotate(new Vector3 {z = rotation });
    }

    public override void Create()
    {
        this.InitializeComponent();
    }
}
