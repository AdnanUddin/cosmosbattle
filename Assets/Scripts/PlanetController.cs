using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
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

    private int numberOfUnits = 0;

    private float lastUnitSpawn;

    private bool isSelected;

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
        Debug.Log("Send enemies");
        int unitsToDeploy = this.numberOfUnits;

        for (int i = 0; i < unitsToDeploy; i++)
        {
            GameObject spaceship = GameObject.Instantiate(spaceShipUnit, this.transform.position + (Random.insideUnitSphere * 0.2f), Random.rotationUniform);
            // spaceship.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            var spaceshipController = spaceship.GetComponent<SpaceShipController>();

            spaceship.tag = this.tag;
            spaceshipController.source = this.gameObject;
            spaceshipController.target = target;
        }

        this.SubtractUnit(unitsToDeploy, null);
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
}
