﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolarTraders;
using System;

public class UniverseManager : MonoBehaviour
{
    public int SeedNumber = 100;

    public GameObject orbitPrefab;
    public GameObject yellowStarPrefab;

    private AudioController ac;
    private List<VictoryCondition> victories;

    private PlayerManager player;
    private ScreenManager screen;

    public GameObject currentlySelectedGO;

    public PlanetaryBody lastSelectedBody;
    public Ship lastSelectedShip;

    // Awake called before Start
    private void Awake()
    {
        // Seed our universe
        UnityEngine.Random.InitState(SeedNumber);

        victories = new List<VictoryCondition>();
    }

    // Start is called before the first frame update
    void Start()
    {

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
                GameObject bodyGO = CreateBodyGameObject(bodyPos, curr_body, isStar);

                // Create the orbit
                GameObject orbitGO = CreateOrbitPath(curr_body.Name + "Orbit", j + 1, this.gameObject);

                // Add it to our Dict
                curr_sys.BodyToObjectMap.Add(curr_sys.Bodies[j], bodyGO);
            }
        }

        ac = FindObjectOfType<AudioController>();
        if (ac == null)
        {
            Debug.LogError("No Audio Controller found!!");
        }

        foreach (VictoryCondition vic in FindObjectsOfType<VictoryCondition>())
        {
            Debug.Log("We found one!");
            victories.Add(vic);
        }

        player = FindObjectOfType<PlayerManager>();
        if (player == null)
        {
            throw new System.Exception("No player found!");
        }

        screen = FindObjectOfType<ScreenManager>();
        if (screen == null)
        {
            throw new System.Exception("No Screen found!");
        }

        UpdateColonisedPlanets();
    }

    public void UpdateColonisedPlanets()
    {
        //Debug.Log("Updating colonised planets:");
        foreach (PlanetaryBody body in Universe.Instance.Systems[0].Bodies)
        {
            if (body is Planet)
            { 
                if (((Planet) body).Colonised)
                {
                    if (!player.colonisedPlanets.Contains((SolarTraders.Planet)body))
                    {
                        //Debug.Log("Added planet " + body);
                        player.colonisedPlanets.Add((SolarTraders.Planet)body);
                    }
                }
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
            distance = (planetNum) * 5;
            angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        }

        // Do maths
        return new Vector3(distance * Mathf.Cos(angle), 0, distance);
    }

    // Creates a GameObject and sets its position
    private GameObject CreateBodyGameObject(Vector3 pos, PlanetaryBody body, bool isStar)
    {
        GameObject toReturn;
        if (isStar)
        {
            //Debug.Log("Creating star");
            toReturn = Instantiate(yellowStarPrefab);
        }
        else
        {
            toReturn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        toReturn.transform.position = pos;
        toReturn.name = body.Name;
        toReturn.tag = "Body";
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

        if (orbitNumber == 1)
        {
            // Star, so no orbit needed
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
            if (currentlySelectedGO != null && currentlySelectedGO.Equals(hit.transform.gameObject))
            {
                // Double clicked
                if(currentlySelectedGO.tag.Equals("Body"))
                {
                    Debug.Log("Double Clicked on " + lastSelectedBody);
                    screen.ShowSelectionDialogue(lastSelectedBody);
                } 
                else if (currentlySelectedGO.tag.Equals("Ship"))
                {
                    Debug.Log("Double Clicked on " + lastSelectedShip);
                    screen.ShowSelectionDialogue(lastSelectedShip);
                }

                ac.PlaySoundEffect(ac.SoundFX[0], false);
            }
            else
            {
                currentlySelectedGO = hit.transform.gameObject;
                if (currentlySelectedGO.tag.Equals("Body"))
                {
                    lastSelectedBody = Universe.Instance.Systems[0].GetBodyFromGameObject(currentlySelectedGO);
                    Debug.Log("Clicked on " + lastSelectedBody);
                } 
                else if (currentlySelectedGO.tag.Equals("Ship")) 
                {
                    lastSelectedShip = player.shipMan.GetShipFromGameObject(currentlySelectedGO);
                    Debug.Log("Clicked on " + lastSelectedShip);
                }

                ac.PlaySoundEffect(ac.SoundFX[1], false);
              
            }
        } 


        if (victories != null)
        {
            foreach (VictoryCondition vic in victories)
            {
                if (vic.PlayerMeetsConditions(player))
                {
                    // Player has won!
                    Debug.Log("Player wins!");
                }
            }
        }
    }
}
