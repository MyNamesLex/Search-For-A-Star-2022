using UnityEngine;

public class EnvironmentObjects : MonoBehaviour
{
    public GameObject[] Lanes;
    public GameObject[] BackLanes;
    public GameObject[] EnvironmentObjectsPrefabs;
    public float TimeUntilNewPass;
    private float OGTime;
    public int ZMoveForce;

    [Header("Prefab Adjustments")]
    public float BoatYPos = 4;
    public float IslandYPos = 1;

    public void Start()
    {
        OGTime = TimeUntilNewPass;
    }
    public void Update()
    {
        if (TimeUntilNewPass >= 0)
        {
            TimeUntilNewPass -= Time.deltaTime;
        }
        else
        {
            TimeUntilNewPass = OGTime;
            int rng = Random.Range(0, 2);
            switch (rng)
            {
                case 0:
                    PickLane();
                    break;
                case 1:
                    PickBackLane();
                    break;
            }
        }
    }

    public void PickLane()
    {
        TimeUntilNewPass = OGTime;
        int rng = Random.Range(0, Lanes.Length);
        GameObject g = PickObject();
        g.GetComponent<EnvironmentFloater>().MoveForce = new Vector3(0, 0, -ZMoveForce);
        switch (rng)
        {
            case 0:
                g.transform.position = Lanes[0].transform.position;
                g.transform.rotation = Lanes[0].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 1:
                g.transform.position = Lanes[1].transform.position;
                g.transform.rotation = Lanes[1].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 2:
                g.transform.position = Lanes[2].transform.position;
                g.transform.rotation = Lanes[2].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 3:
                g.transform.position = Lanes[3].transform.position;
                g.transform.rotation = Lanes[3].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 4:
                g.transform.position = Lanes[4].transform.position;
                g.transform.rotation = Lanes[4].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 5:
                g.transform.position = Lanes[5].transform.position;
                g.transform.rotation = Lanes[5].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 6:
                g.transform.position = Lanes[6].transform.position;
                g.transform.rotation = Lanes[6].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
        }

    }

    public void PickBackLane()
    {
        TimeUntilNewPass = OGTime;
        int rng = Random.Range(0, Lanes.Length);
        GameObject g = PickObject();
        if (g == EnvironmentObjectsPrefabs[0])
        {
            // island prefab has issues with back lane, makes the trees no longer attached to the land
            Debug.Log("PickLane recursion");
            PickBackLane();
            return;
        }
        g.GetComponent<EnvironmentFloater>().MoveForce = new Vector3(0, 0, ZMoveForce);
        switch (rng)
        {
            case 0:
                g.transform.position = BackLanes[0].transform.position;
                g.transform.rotation = BackLanes[0].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 1:
                g.transform.position = BackLanes[1].transform.position;
                g.transform.rotation = BackLanes[1].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 2:
                g.transform.position = BackLanes[2].transform.position;
                g.transform.rotation = BackLanes[2].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 3:
                g.transform.position = BackLanes[3].transform.position;
                g.transform.rotation = BackLanes[3].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 4:
                g.transform.position = BackLanes[4].transform.position;
                g.transform.rotation = BackLanes[4].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 5:
                g.transform.position = BackLanes[5].transform.position;
                g.transform.rotation = BackLanes[5].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
            case 6:
                g.transform.position = BackLanes[6].transform.position;
                g.transform.rotation = BackLanes[6].transform.rotation;
                g = ObjectAdjust(g);

                Instantiate(g, gameObject.transform);
                break;
        }
    }



    public GameObject PickObject()
    {
        int rng = Random.Range(0, EnvironmentObjectsPrefabs.Length + 1);
        switch (rng)
        {
            case 0:
                EnvironmentObjectsPrefabs[0].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[0];
            case 1:
                EnvironmentObjectsPrefabs[1].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[1];
            case 2:
                EnvironmentObjectsPrefabs[2].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[2];
            case 3:
                EnvironmentObjectsPrefabs[3].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[3];
            case 4:
                EnvironmentObjectsPrefabs[4].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[4];
            case 5:
                EnvironmentObjectsPrefabs[5].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[5];
            case 6:
                EnvironmentObjectsPrefabs[6].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[6];
            case 7:
                EnvironmentObjectsPrefabs[7].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[7];
            case 8:
                EnvironmentObjectsPrefabs[8].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[8];
            case 9:
                EnvironmentObjectsPrefabs[9].gameObject.tag = "Floater";
                return EnvironmentObjectsPrefabs[9];
            default:
                return EnvironmentObjectsPrefabs[0];
        }
    }

    public GameObject ObjectAdjust(GameObject g)
    {
        if (g == EnvironmentObjectsPrefabs[8]) // boat prefab spawns in air, needs to be moved down
        {
            Vector3 temp = g.transform.position;
            temp.y -= BoatYPos;
            g.transform.position = temp;
            return g;
        }
        if (g == EnvironmentObjectsPrefabs[0])// island prefab spawns a little sunken into the water, needs to be moved up
        {
            Vector3 temp = g.transform.position;
            temp.y += IslandYPos;
            g.transform.position = temp;
            return g;
        }
        return g;
    }
}
