using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField]
    private GameObject addOnObject;

    [SerializeField]
    private TextMesh numberOfUnitsLabel;

    [SerializeField]
    private bool useAddOn;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float spawnInterval = 1f;

    private int numberOfUnits = 0;

    private float lastUnitSpawn;

    // Start is called before the first frame update
    void Start()
    {
        this.numberOfUnits = 0;
        this.lastUnitSpawn = Time.time;
        this.useAddOn = false;
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
}
