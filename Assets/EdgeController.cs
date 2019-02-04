using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : ControllerBase
{
    [SerializeField]
    private GameObject first;
    
    [SerializeField]
    private GameObject second;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeComponent()
    {
        var firstVector = new Vector3(3, 0, 0);
        var firstObject = Instantiate(first, firstVector, Quaternion.identity);
        
        var secondVector = new Vector3(-5, 0, 0);
        var secondObject = Instantiate(second, secondVector, Quaternion.identity);
        
        var firstController = firstObject.GetComponent<PlanetController>();
        var secondController = secondObject.GetComponent<PlanetController>();

        var distance = CalculationHelper.GetDistance(firstController, secondController);
        var rotation = CalculationHelper.GetAngle(firstController, secondController) - 90f; // offset from global space
        var startingPosition = (CalculationHelper.GetDirectionVector(firstController, secondController) / 2) + firstVector;
        
        this.gameObject.transform.localScale = new Vector3(0.1f, distance, 0.1f);
        this.gameObject.transform.position = startingPosition;
        this.gameObject.transform.Rotate(new Vector3 {z = rotation });
    }

    public override void Create()
    {
        this.InitializeComponent();
    }
}
