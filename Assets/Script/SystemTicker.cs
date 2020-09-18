using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTicker : MonoBehaviour
{
    private List<GameSystem> systems = new List<GameSystem>();
    private void Awake()
    {
        GetComponentsInChildren<GameSystem>(systems);
        foreach (GameSystem system in systems)
        {
            system.Initialize(this);
        }
    }
    private void Update()
    {
        for (int i = 0; i < systems.Count; i++)
        {
            systems[i].Tick();
        }
    }

}
