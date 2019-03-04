using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolarTraders;

public class UniverseManager : MonoBehaviour
{
    Universe uni;
    // Start is called before the first frame update
    void Start()
    {
        uni = new Universe();
        uni.AddSolarSystem();

        for (int i = 0; i < uni.Systems.Count; i++)
        {
            // Iterate through Solar systems
            SolarSystem curr_sys = uni.Systems[i];
            for (int j = 0; j < curr_sys.Bodies.Count; j++)
            {
                GameObject bodyGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                float distance;
                float angle;

                if (curr_sys.Bodies[j] is Star)
                {
                    distance = 0;
                    angle = 0;
                }
                else
                {
                    distance = Random.Range(0, curr_sys.Maximum_radius);
                    angle = Random.Range(0, 2 * Mathf.PI);
                }

                Vector3 cartesianPosition = new Vector3(distance * Mathf.Cos(angle), 0, distance);
                bodyGO.transform.position = cartesianPosition;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
