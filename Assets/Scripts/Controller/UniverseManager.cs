﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolarTraders;

public class UniverseManager : MonoBehaviour
{
    public int SeedNumber = 100;

    public GameObject orbitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Seed our universe
        Random.InitState(SeedNumber);

        Universe uni = Universe.Instance;
        uni.AddSolarSystem();

        for (int i = 0; i < uni.Systems.Count; i++)
        {
            // Iterate through Solar systems
            SolarSystem curr_sys = uni.Systems[i];
            for (int j = 0; j < curr_sys.Bodies.Count; j++)
            {
                // Get the Body
                PlanetaryBody curr_body = curr_sys.Bodies[j];

                // Get the position
                bool isStar = curr_body is Star;
                Vector3 bodyPos = RandomCartesianPosition(isStar, j);

                // Create the Game Object
                GameObject bodyGO = CreateBodyGameObject(bodyPos, curr_body);

                // Create the orbit
                GameObject orbitGO = CreateOrbitPath(curr_body.Name + "Orbit", j + 1, this.gameObject);

                // Add it to our Dict
                curr_sys.BodyToObjectMap.Add(curr_sys.Bodies[j], bodyGO);
            }
        }
    }

    // Creates a random cartesian position inside a certain radius, which is 0 if it needs to be a star
    private Vector3 RandomCartesianPosition(bool isStar, int planetNum)
    {
        float distance;
        float angle;

        if (isStar)
        {
            // Center of the System
            distance = 0;
            angle = 0;
        }
        else
        {
            // Random position
            distance = (planetNum + 1) * 5;
            angle = Random.Range(0, 2 * Mathf.PI);
        }

        // Do maths
        return new Vector3(distance * Mathf.Cos(angle), 0, distance);
    }

    // Creates a GameObject and sets its position
    private GameObject CreateBodyGameObject(Vector3 pos, PlanetaryBody body)
    {
        GameObject toReturn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        toReturn.transform.position = pos;
        toReturn.name = body.Name;
        toReturn.transform.SetParent(this.transform);

        return toReturn;
    }

    private GameObject CreateOrbitPath(string Name, int orbitNumber, GameObject parent)
    {
        // Sanity Check
        if (orbitPrefab == null)
        {
            Debug.LogError("No Orbit Prefab assigned!");
            return null;
        }

        GameObject orbit = Instantiate(orbitPrefab);

        orbit.name = Name;
        orbit.transform.localScale = orbit.transform.localScale * orbitNumber;
        orbit.transform.SetParent(parent.transform);

        return orbit;
    }

    // Update is called once per frame
    void Update()
    {

        // Mouse interaction with the Universe
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
        {
            PlanetaryBody body = Universe.Instance.Systems[0].GetBodyFromGameObject(hit.transform.gameObject);
            Debug.Log("Clicked on " + body.ToString());
        }
    }
}