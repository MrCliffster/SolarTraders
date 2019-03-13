using System.Collections.Generic;
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

    public void BuildProbe()
    {
        Probe probe = new Probe();

        ShipList.Add(probe);

        GameObject probeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // probeObject.transform.position = something
        probeObject.transform.SetParent(this.transform);

        ShipToObjectMap.Add(probe, probeObject);
    }

    public void BuildDrone()
    {
        Drone drone = new Drone();

        ShipList.Add(drone);

        GameObject droneObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        // Position stuff
        droneObject.transform.SetParent(this.transform);

        ShipToObjectMap.Add(drone, droneObject);
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
