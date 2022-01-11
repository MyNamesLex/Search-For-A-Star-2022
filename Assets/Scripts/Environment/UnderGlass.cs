using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGlass : MonoBehaviour
{
    public GameObject[] UnderGlassObj;
    public GameObject SpawnLocation;
    public float ChanceToHappen;
    public float OccurenceTimer;
    private float OGOccurenceTimer;
    public List<GameObject> FishInScene;

    public void Start()
    {
        OGOccurenceTimer = OccurenceTimer;
        SpawnChance();
    }

    public void Update()
    {
        if(OccurenceTimer > 0)
        {
            OccurenceTimer -= Time.deltaTime;
        }
        else
        {
            OccurenceTimer = OGOccurenceTimer;
            SpawnChance();
        }
    }

    public void SpawnChance()
    {
        if (FishInScene.Count <= 50)
        {
            float rng = Random.Range(0, 100);
            int RandomObj = Random.Range(0, UnderGlassObj.Length);
            if (rng <= ChanceToHappen)
            {
                GameObject g;
                g = Instantiate(UnderGlassObj[RandomObj], gameObject.transform);
                g.GetComponent<EnvironmentFloater>().UnderGlass = true;
                g.transform.position = SpawnLocation.transform.position;
                FishInScene.Add(g);
               // Debug.Log("Fish Spawned");
            }
        }
        else
        {
           //Debug.LogWarning("Max Amount of fish in scene! List length = " + FishInScene.Count);
        }
    }
}
