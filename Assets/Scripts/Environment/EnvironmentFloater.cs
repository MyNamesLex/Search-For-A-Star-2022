using UnityEngine;
using System.Collections;

public class EnvironmentFloater : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 MoveForce;
    public float speed = 5;
    public bool UnderGlass = false;
    public Vector3[] Points;
    public float timeout;
    public float TimeUntilNewPos;
    private float OGTimeUntilNewPos;
    public int rngpos;
    public void Start()
    {
        if (UnderGlass == true)
        {
            OGTimeUntilNewPos = TimeUntilNewPos;
           // StartCoroutine(Disappear());
        }

        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (!UnderGlass)
        {
            rb.AddForce(MoveForce * speed, ForceMode.Impulse);

            Vector2 v2 = new Vector2(rb.velocity.x, rb.velocity.z);

            Vector3 ClampedSpeed = Vector3.ClampMagnitude(v2, speed);

            rb.velocity = new Vector3(ClampedSpeed.x, rb.velocity.y, ClampedSpeed.y);
        }
        else if(UnderGlass == true)
        {
            MoveTo();
        }
    }

    IEnumerator Disappear()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeout);
            UnderGlass = false;
            transform.LookAt(MoveForce);
        }
    }

    public void MoveTo()
    {
        if (TimeUntilNewPos <= 0)
        {
            TimeUntilNewPos = OGTimeUntilNewPos;
            rngpos = Random.Range(0, Points.Length);
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, Points[rngpos], step);
            transform.LookAt(Points[rngpos]);
            TimeUntilNewPos -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("UnderGlassPoint"))
        {
            TimeUntilNewPos = 0;
        }
    }
}
