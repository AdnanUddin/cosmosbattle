using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Ray ray;

    private GameObject playerSelectedPlanet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    this.SelectingAPlanet(hit);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            if (playerSelectedPlanet != null && hits.Length == 0) // clicked in the void
            {
                // Deselecting the planet
                playerSelectedPlanet.GetComponentInParent<PlanetController>().ToggleSelection();
                playerSelectedPlanet = null;
            }
            else
            {
                foreach (RaycastHit hit in hits)
                {
                    // Select the player planet
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        if (this.playerSelectedPlanet == null)
                        {
                            this.SelectingAPlanet(hit);
                        }
                        else
                        {
                            playerSelectedPlanet.GetComponentInParent<PlanetController>().SendUnits(hit.collider.transform.gameObject);
                        }
                    }
                    else
                    {
                        if (this.playerSelectedPlanet != null)
                        {
                            playerSelectedPlanet.GetComponentInParent<PlanetController>().SendUnits(hit.collider.transform.gameObject);
                        }
                    }
                }
            }
        }
    }

    void SelectingAPlanet(RaycastHit hit)
    {
        if (playerSelectedPlanet != null && playerSelectedPlanet != hit.collider.gameObject)
        {
            playerSelectedPlanet.GetComponentInParent<PlanetController>().ToggleSelection();
        }

        if (hit.collider.gameObject.GetComponentInParent<PlanetController>().ToggleSelection())
        {
            playerSelectedPlanet = hit.collider.gameObject;
        }
        else
        {
            playerSelectedPlanet = null;
        }

        Debug.Log("Hit a planet");
    }
}
