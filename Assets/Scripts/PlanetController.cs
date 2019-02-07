using System.Collections;
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

    [SerializeField]
    private int unitsToDominate = 10;

    [SerializeField]
    private int unitsDeployBatch = 5;

    private int numberOfUnits = 0;

    private float lastUnitSpawn;

    private bool isSelected;

    private int unitsToDeploy;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        this.numberOfUnits = 0;
        this.lastUnitSpawn = Time.time;
        this.useAddOn = false;
        this.isSelected = false;
    }

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

        if (this.unitsToDeploy > 0 && (this.spawnInterval) < (Time.time - this.lastUnitSpawn / 2f))
        {
            this.DeployUnits();
        }
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
        if (this.tag != "NeutralPlanet" && (this.spawnInterval) < (Time.time - this.lastUnitSpawn))
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
        if (target != this.target)
        {
            Debug.Log("Send enemies");
            this.unitsToDeploy = this.numberOfUnits;
            this.target = target;
        }
    }

    public void DeployUnits()
    {
        var unitsBatch = Mathf.Min(this.unitsToDeploy, this.unitsDeployBatch);

        for (int i = 0; i < unitsBatch; i++)
        {
            GameObject spaceship = GameObject.Instantiate(spaceShipUnit, this.transform.position + (Random.insideUnitSphere * 0.2f), Random.rotationUniform);
            // spaceship.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            var spaceshipController = spaceship.GetComponent<SpaceShipController>();

            spaceship.tag = this.tag;
            spaceshipController.source = this.gameObject;
            spaceshipController.target = this.target;
        }

        this.unitsToDeploy -= unitsBatch;
        this.SubtractUnit(unitsBatch, null);

        if (unitsToDeploy <= 0)
        {
            this.target = null;
        }
    }

    public void SubtractUnit(int numberOfUnits, string unitSourceTag)
    {
        this.numberOfUnits -= numberOfUnits;
        if (!string.IsNullOrEmpty(unitSourceTag))
        {
            if (this.numberOfUnits <= 0)
            {
                if (this.tag == "NeutralPlanet")
                {
                    this.numberOfUnits = 0;
                    this.SetTag(unitSourceTag);
                }
                else if (this.tag != unitSourceTag)
                {
                    this.numberOfUnits = unitsToDominate;
                    this.SetTag("NeutralPlanet");
                }
            }
        }
    }

    public void AddUnit(int numberOfUnits)
    {
        this.numberOfUnits += numberOfUnits;
    }

    public void SetTag(string tag)
    {
        this.tag = tag;
        this.transform.GetChild(0).tag = tag;
    }

    public override void Create()
    {
        this.numberOfUnits = 0;
        this.lastUnitSpawn = Time.time;
        this.useAddOn = false;
        this.isSelected = false;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
