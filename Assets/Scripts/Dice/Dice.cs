using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    public LayerMask FloorMask;
    private Rigidbody rb;
    public bool stop = true;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject DiceCamTexture;
    public Board board;
    public GameObject Radius1Trigger;
    public GameObject Radius2Trigger;
    public GameObject Radius3Trigger;
    public int xmax;
    public int ymax;
    public int zmax;
    [Header("Effects")]
    public DiceVFX Dicevfx;
    public bool OnFour = false;
    [Header("Audio")]
    public AudioSource audioSource;
    public float volume;
    public AudioClip DiceLand;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        stop = true;
        int randomx = UnityEngine.Random.Range(0, xmax);
        int randomy = UnityEngine.Random.Range(0, ymax);
        int randomz = UnityEngine.Random.Range(0, zmax);
        rb.AddForce(new Vector3(randomx, randomy, randomz), ForceMode.Impulse);
        //Debug.Log(randomx + " , " + randomy + " , " + randomz);
    }

    public void Update()
    {
        Debug.DrawRay(right.transform.position, right.transform.forward, Color.blue); // three
        Debug.DrawRay(right.transform.position, right.transform.right, Color.green); // two
        Debug.DrawRay(left.transform.position, left.transform.forward, Color.black); // five
        Debug.DrawRay(left.transform.position, left.transform.right, Color.red); // six
        Debug.DrawRay(up.transform.position, up.transform.forward, Color.gray); // four
        Debug.DrawRay(down.transform.position, down.transform.forward, Color.cyan); // one


        if (rb.velocity.magnitude != 0)
        {
            DiceCamTexture.SetActive(true);
            stop = false;
        }

        if (rb.velocity.magnitude == 0)
        {
            DiceCamTexture.SetActive(false);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BoardPiece") || other.CompareTag("Floor") || other.CompareTag("Wall"))
        {
            audioSource.PlayOneShot(DiceLand, volume);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("BoardPiece"))
        {
            if (rb.velocity.magnitude == 0 && stop == false)
            {
                RaycastHit hit;

                if (Physics.Raycast(right.transform.position, right.transform.forward, out hit, 90, FloorMask)) // five
                {
                    if (hit.collider.tag == "Floor" && stop == false)
                    {
                        stop = true;
                        Debug.Log("Landed on five");
                    }
                    if (other.CompareTag("BoardPiece") && stop == false)
                    {
                        LandedOnFive();
                        other.GetComponent<Box>().OnClick(gameObject);
                    }
                }
                if (Physics.Raycast(left.transform.position, left.transform.forward, out hit, 90, FloorMask)) // three
                {
                    if (hit.collider.tag == "Floor" && stop == false)
                    {
                        stop = true;
                        Debug.Log("Landed on three");
                    }
                    if (other.CompareTag("BoardPiece") && stop == false)
                    {
                        LandedOnThree();
                        other.GetComponent<Box>().OnClick(gameObject);
                    }
                }
                if (Physics.Raycast(up.transform.position, up.transform.forward, out hit, 90, FloorMask)) // one
                {
                    if (hit.collider.tag == "Floor" && stop == false)
                    {
                        stop = true;
                        Debug.Log("Landed on one");
                    }
                    if (other.CompareTag("BoardPiece") && stop == false)
                    {
                        GenerateRNG(); // one
                        other.GetComponent<Box>().OnClick(gameObject);
                    }
                }
                if (Physics.Raycast(down.transform.position, down.transform.forward, out hit, 90, FloorMask)) // four
                {
                    if (hit.collider.tag == "Floor" && stop == false)
                    {
                        stop = true;
                        Debug.Log("Landed on four");
                    }
                    if (other.CompareTag("BoardPiece") && stop == false)
                    {
                        if (other.GetComponent<Box>().IsDangerous == true)
                        {
                            other.GetComponent<Box>().OnClick(gameObject);
                        }
                        else
                        {
                            LandedOnFour(other.gameObject);
                            other.GetComponent<Box>().OnClick(gameObject);
                        }
                    }
                }

                if (Physics.Raycast(left.transform.position, left.transform.right, out hit, 90, FloorMask)) // two
                {
                    if (hit.collider.tag == "Floor" && stop == false)
                    {
                        stop = true;
                        Debug.Log("Landed on two");
                    }
                    if (other.CompareTag("BoardPiece") && stop == false)
                    {
                        LandedOnTwo();
                        other.GetComponent<Box>().OnClick(gameObject);
                    }
                }

                if (Physics.Raycast(left.transform.position, right.transform.right, out hit, 90, FloorMask)) // six
                {
                    if (hit.collider.tag == "Floor" && stop == false)
                    {
                        stop = true;
                        Debug.Log("Landed on six");
                    }
                    if (other.CompareTag("BoardPiece") && stop == false)
                    {
                        LandedOnSix(other.gameObject);
                        other.GetComponent<Box>().OnClick(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoardPiece"))
        {
            if (OnFour == true)
            {
                OnFour = false;
            }
        }
    }

    //effects

    public void LandedOnOne(int rng)
    {
        if (stop == false)
        {
            stop = true;
            Dicevfx.DiceEffectOne();
            board._grid[rng].gameObject.GetComponent<Box>().IsDangerous = true;
            board._grid[rng].gameObject.GetComponent<ParticleSystem>().Play();
            Debug.Log(board._grid[rng].gameObject.name);
        }
    }

    public void GenerateRNG()
    {
        int rng = (UnityEngine.Random.Range(0, board._grid.Length));
        if (board._grid[rng].gameObject.GetComponent<Box>().IsDangerous)
        {
            GenerateRNG();
        }
        if(board._grid[rng].gameObject.GetComponent<Box>()._textDisplay.text == "Mine Clear")
        {
            GenerateRNG();
        }
        else if(board._grid[rng].gameObject.GetComponent<Box>()._textDisplay.text != "Mine Clear" && board._grid[rng].gameObject.GetComponent<Box>().IsDangerous == false)
        {
            LandedOnOne(rng);
        }
    }
    public void LandedOnTwo()
    {
        if (stop == false)
        {
            stop = true;
            Dicevfx.DiceEffectTwo();
            Radius2Trigger.gameObject.transform.position = gameObject.transform.position;
            Radius2Trigger.SetActive(true);
        }
    }

    public void LandedOnThree()
    {
        if (stop == false)
        {
            stop = true;
            Dicevfx.DiceEffectThree();
            Radius3Trigger.gameObject.transform.position = gameObject.transform.position;
            Radius3Trigger.SetActive(true);
        }
    }
    public void LandedOnFour(GameObject g)
    {
        if (stop == false)
        {
            OnFour = true;
            Dicevfx.DiceEffectFour();
            stop = true;
            g.GetComponent<Box>().IsDangerous = true;
        }
    }
    public void LandedOnFive()
    {
        if (stop == false)
        {
            stop = true;
            Dicevfx.DiceEffectFive();
            Radius1Trigger.gameObject.transform.position = gameObject.transform.position;
            Radius1Trigger.SetActive(true);
        }
    }
    public void LandedOnSix(GameObject g)
    {
        if (stop == false)
        {
            stop = true;
            Dicevfx.DiceEffectSix(g);
            if (g.GetComponent<Box>().IsDangerous == true)
            {
                g.GetComponent<ParticleSystem>().Play();
                g.GetComponent<Box>().IsDangerous = false;
                Debug.Log("square was dangerous");
            }
        }
    }
}
