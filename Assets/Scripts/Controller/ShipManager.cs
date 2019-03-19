﻿using System.Collections.Generic;
using UnityEngine;
using SolarTraders;
using System.Linq;

public class ShipManager : MonoBehaviour
{
    List<Ship> ShipList;
    Dictionary<Ship, GameObject> ShipToObjectMap;

    // Start is called before the first frame update
    void Start()
    {
        ShipList = new List<Ship>();
        ShipToObjectMap = new Dictionary<Ship, GameObject>();
    }

    public int GetNumShips()
    {
        return ShipList.Count;
    }

    public void BuildProbe()
    {
        Probe probe = new Probe(Probe.GetProbeName());

        ShipList.Add(probe);

        GameObject probeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // probeObject.transform.position = something
        probeObject.transform.SetParent(this.transform);
        probeObject.name = probe.Name;

        ShipToObjectMap.Add(probe, probeObject);
    }

    public void buildDroneGUIWrapper(int typeInt)
    {
        switch (typeInt)
        {
            case 0:
                BuildDrone(Drone.Type.Mining);
                break;
            case 1:
                BuildDrone(Drone.Type.Refining);
                break;
            case 2:
                BuildDrone(Drone.Type.Farming);
                break;
        }
    }

    public void BuildDrone(Drone.Type type)
    {
        Drone drone = new Drone(Drone.GetDroneName(), type);

        ShipList.Add(drone);

        GameObject droneObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        // Position stuff
        droneObject.transform.SetParent(this.transform);

        ShipToObjectMap.Add(drone, droneObject);
    }

    public void BuildColonyShip()
    {
        ColonyShip ship = new ColonyShip(ColonyShip.GetColonyShipName());

        ShipList.Add(ship);

        GameObject shipObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //Position stuff
        shipObject.transform.SetParent(this.transform);

        ShipToObjectMap.Add(ship, shipObject);
    }

    public Ship GetShipFromGameObject(GameObject obj)
    {
        if (ShipToObjectMap.ContainsValue(obj))
        {
            var key = ShipToObjectMap.Where(item => item.Value.Equals(obj)).Select(item => item.Key).FirstOrDefault();
            return key;
        }
        return null;
    }
}
