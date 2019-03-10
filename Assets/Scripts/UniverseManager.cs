using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolarTraders;

public class UniverseManager : MonoBehaviour
{
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
                GameObject bodyGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                curr_sys.BodyToObjectMap.Add(curr_sys.Bodies[j], bodyGO);

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

                bodyGO.transform.position = CartesianPosition(distance, angle);
            }
        }
    }

    private Vector3 CartesianPosition(float distance, float angle)
    {
        return new Vector3(distance * Mathf.Cos(angle), 0, distance);
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
