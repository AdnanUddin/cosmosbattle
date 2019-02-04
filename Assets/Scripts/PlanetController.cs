﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : ControllerBase
{
    public const float ZAxis = 0.0f;

    [SerializeField]
    private GameObject addOnObject;

    [SerializeField]
    private GameObject planetSelectionObject;

    [SerializeField]
    private GameObject spaceShipUnit;

    [SerializeField]
    private TextMesh numberOfUnitsLabel;

    [SerializeField]
    private bool useAddOn;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float spawnInterval = 1f;

    private int numberOfUnits = 0;

    private float lastUnitSpawn;

    private bool isSelected;

    // Start is called before the first frame update
    // void Start()
    // {
    //     this.numberOfUnits = 0;
    //     this.lastUnitSpawn = Time.time;
    //     this.useAddOn = false;
    //     this.isSelected = false;
    // }

    // Update is called once per frame
    void Update()
    {
        if (this.useAddOn)
        {
            addOnObject.SetActive(true);
        }
        else
        {
            addOnObject.SetActive(false);
        }

        this.UpdateNumberOfUnitsLabel();
    }

    void FixedUpdate()
    {
        if (!useAddOn)
        {
            this.UnitSpawnCheck();
        }
    }

    void UnitSpawnCheck()
    {
        if ((this.spawnInterval) < (Time.time - this.lastUnitSpawn))
        {
            this.SpawnUnit();

            this.lastUnitSpawn = Time.time;
        }
    }

    void SpawnUnit()
    {
        this.numberOfUnits++;
    }

    void UpdateNumberOfUnitsLabel()
    {
        numberOfUnitsLabel.text = numberOfUnits.ToString();
    }

    public bool ToggleSelection()
    {
        this.planetSelectionObject.SetActive(!this.planetSelectionObject.activeSelf);

        this.isSelected = this.planetSelectionObject.activeSelf;
        return isSelected;
    }

    public void SendUnits(GameObject target)
    {
        Debug.Log("Send enemies");
        int unitsToDeploy = this.numberOfUnits;

        for (int i = 0; i < unitsToDeploy; i++)
        {
            GameObject spaceship = GameObject.Instantiate(spaceShipUnit, this.transform.position + Random.insideUnitSphere, Quaternion.LookRotation(Vector3.up, -Vector3.forward));
           // spaceship.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            spaceship.GetComponent<SpaceShipController>().target = target;
        }

        this.numberOfUnits -= unitsToDeploy;
    }

    public override void Create()
    {
        this.numberOfUnits = 0;
        this.lastUnitSpawn = Time.time;
        this.useAddOn = false;
        this.isSelected = false;
    }

    public override void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
