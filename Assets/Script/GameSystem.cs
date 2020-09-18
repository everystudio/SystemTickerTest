using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public abstract class GameSystem : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
        public float updateDelay = 0;
    }

    public Settings systemSettings;

    private float currentUpdateTime;

    private bool isInitialized;

    private bool pauzedUpdate;

    public abstract void OnLoadSystem();
    public virtual void OnTick() { }

    protected void Pauze(bool state)
    {
        pauzedUpdate = state;

        if (state)
        {
            currentUpdateTime = systemSettings.updateDelay;
        }
    }

    public void Initialize(SystemTicker _ticker)
    {
        if (!isInitialized)
        {
            OnLoadSystem();
            isInitialized = true;
        }
    }

    public void Tick()
    {
        if (pauzedUpdate)
            return;

        if (systemSettings.updateDelay == 0 || currentUpdateTime >= systemSettings.updateDelay)
        {
            OnTick();
            currentUpdateTime = 0;
        }
        else
        {
            currentUpdateTime += Time.deltaTime;
        }
    }
}