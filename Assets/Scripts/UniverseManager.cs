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
        Init();
    }

    void Init()
    {
        for (int i = 0; i < uni.Systems.Count; i++)
        {
            uni.Systems.get
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
