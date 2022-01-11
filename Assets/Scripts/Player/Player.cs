using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float MaxSpeed;
    public float SprintSpeed;
    public bool OnGround = true;
    public bool DoubleJumpAvailable = true;
    public bool isSprinting = false;
    private bool Changed = false;
    public bool isSliding = false;
    public float SlideForce;
    public float JumpForce;
    private Rigidbody rb;
    public Vector3 Movement;

    [Header("Camera")]
    public CameraController Cam;
    public BirdsEyeCam bec;

    [Header("Settings")]
    public GameObject PauseObjects;
    public GameObject AccessibilityObjects;
    public GameObject DiceCam;

    [Header("Audio")]
    public AudioSource SFXAudioSource;
    public float volume;

    [Header("Audioclips")]
    public AudioClip JumpSFX;
    public AudioClip DiceHitSFX;

    [Header("Particles")]
    public ParticleSystem particles;

    [Header("Can Hit Dice?")]
    public bool CanHitDice = false;
    public GameObject Dice;
    public float DiceSpeed;
    public float DiceNudgeSpeed;
    public float TimeUntilNudgeAvailable;
    private float OGTimeUntilNudgeAvailable;
    public bool CanNudge = true;
    public Texture DiceCamTexture;
    public DiceVFX dicevfx;

    [Header("PlayerTwo")]
    public bool isPlayerTwo = false;

    [Header("Accessibility Player Effects")]
    public Accessibility access;
    public bool CamShakeEnabled = true;

    [Header("Check for mine")]
    public bool CheckMine = false;
    public GameObject CurrentlyOnSquare;
    public Board board;

    public void Start()
    {
        OGTimeUntilNudgeAvailable = TimeUntilNudgeAvailable;
        if (SceneManager.GetActiveScene().name != "TwoPlayer")
        {
            Cam.enabled = true;
            bec.birdseyecam.enabled = false;
        }
        Time.timeScale = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Developer Comment - I was considering having a two player mode in the game
        //but this didn't really fit the pacing and overall feel of the game, there was
        //two ideas for this, first one being two people share the keyboard and there is
        //remnants of that still in the code that i left in here deliberatly to show
        //what I was thinking of. As you can see i was running out of keys to give to the second player
        // that will feel nice and conveniant to use and i eventually scrapped it.
        //
        //The second version of the two player was to be turn based and take turns using the same controls
        //scheme but this however did not fit the pacing of the game and felt as if it didn't really belong
        //in the game

        // Movement
        if (isPlayerTwo == false)
        {
            float Horizontal = Input.GetAxis("Horizontal") * MaxSpeed * Time.deltaTime;
            float Vertical = Input.GetAxis("Vertical") * MaxSpeed * Time.deltaTime;

            Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            Movement.y = 0f;
        }
        else
        {
            float Horizontal = Input.GetAxis("P2Horizontal") * MaxSpeed * Time.deltaTime;
            float Vertical = Input.GetAxis("P2Vertical") * MaxSpeed * Time.deltaTime;

            Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            Movement.y = 0f;
        }

        if (isSprinting)
        {
            rb.AddForce(Movement * SprintSpeed, ForceMode.Impulse);

            Vector2 v2 = new Vector2(rb.velocity.x, rb.velocity.z);

            Vector3 ClampedSpeed = Vector3.ClampMagnitude(v2, SprintSpeed);

            rb.velocity = new Vector3(ClampedSpeed.x, rb.velocity.y, ClampedSpeed.y);
        }

        else
        {
            rb.AddForce(Movement * MaxSpeed, ForceMode.Impulse);

            Vector2 v2 = new Vector2(rb.velocity.x, rb.velocity.z);

            Vector3 ClampedSpeed = Vector3.ClampMagnitude(v2, MaxSpeed);

            rb.velocity = new Vector3(ClampedSpeed.x, rb.velocity.y, ClampedSpeed.y);
        }

        OnGround = Physics.Raycast((new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z)), Vector3.down, 2.8f, 1 << LayerMask.NameToLayer("Floor"));

        if (Movement.magnitude != 0)
        {
            //SFXAudioSource.Play() -> play walk moving sfx;
        }

        if (Movement.magnitude == 0)
        {
            //SFXAudioSource.Stop() -> stop walk moving sfx;
        }

        // Rotate Player

        Quaternion CamRotation = Cam.rotation;
        CamRotation.x = 0f;
        CamRotation.z = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        if (Changed)
        {
            StartCoroutine(WaitUntilGrounded());
        }
    }
    void Update()
    {
        //Get Accessibility features
        if(access == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("AccessibilityTag");
            access = g.GetComponent<Accessibility>();

            GetCameraShake(access.CameraShakeEnabled);
        }
        //double jump
        if (OnGround)
        {
            DoubleJumpAvailable = true;
        }

        //nudge

        if(CanNudge == false)
        {
            if(TimeUntilNudgeAvailable > 0)
            {
                TimeUntilNudgeAvailable -= Time.deltaTime;
            }
            else
            {
                CanNudge = true;
                TimeUntilNudgeAvailable = OGTimeUntilNudgeAvailable;
            }
        }

        //check for mine

        if(Input.GetKeyDown(KeyCode.Q) && CheckMine == true)
        {
            if(CurrentlyOnSquare.GetComponent<Box>().IsDangerous == true)
            {
                CurrentlyOnSquare.GetComponent<Box>().IsDangerous = false;
                CurrentlyOnSquare.GetComponent<Box>()._textDisplay.gameObject.SetActive(true);
                CurrentlyOnSquare.GetComponent<Box>()._textDisplay.text = "Mine Clear";
                Debug.Log("removed mine");
            }
            else if(CurrentlyOnSquare.GetComponent<Box>().IsDangerous == false)
            {
                Debug.Log("Mine was not dangerous");
                CurrentlyOnSquare.GetComponent<Box>().IsDangerous = true;
                board.OnClickedBox(CurrentlyOnSquare.GetComponent<Box>());
            }
        }

        // Dice Controls

        if (isPlayerTwo == false)
        {
            if (Input.GetMouseButton(0) && CanHitDice == true)
            {
                if (CamShakeEnabled == true)
                {
                    Cam.ShakeCam();
                }
                Dice.GetComponent<Rigidbody>().velocity = Cam.transform.forward * DiceSpeed;
                SFXAudioSource.PlayOneShot(DiceHitSFX, 1);
            }
            if(Input.GetMouseButtonDown(1) && CanHitDice == false && CanNudge == true)
            {
                CanNudge = false;
                Dice.GetComponent<Rigidbody>().velocity = Cam.transform.forward * DiceNudgeSpeed;
                SFXAudioSource.PlayOneShot(DiceHitSFX, 1);
                dicevfx.DiceNudgeEffect();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && CanHitDice == true)
            {
                Cam.ShakeCam();
                Dice.GetComponent<Rigidbody>().velocity = Cam.transform.forward * DiceSpeed;
            }
        }

        //sprint
        if (isPlayerTwo == false)
        {
            // no toggle sprint

            if (access.ToggleSprintEnabled == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (isSprinting == true)
                    {
                        isSprinting = false;
                    }
                    else
                    {
                        isSprinting = true;
                    }
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Changed = true;
                    if (OnGround && isSprinting == false)
                    {
                        isSprinting = true;
                        Changed = false;
                    }
                    if (OnGround && isSprinting == true)
                    {
                        isSprinting = false;
                        Changed = false;
                    }
                    else if (OnGround == false)
                    {
                        StartCoroutine(WaitUntilGrounded());
                    }
                }
            }
            else if (access.ToggleSprintEnabled == true)
            {
                //toggle sprint
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (isSprinting == true)
                    {
                        isSprinting = false;
                    }
                    else
                    {
                        isSprinting = true;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                if (isSprinting == true)
                {
                    isSprinting = false;
                }
                else
                {
                    isSprinting = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                Changed = true;
                if (OnGround && isSprinting == false)
                {
                    isSprinting = true;
                    Changed = false;
                }
                if (OnGround && isSprinting == true)
                {
                    isSprinting = false;
                    Changed = false;
                }
                else if (OnGround == false)
                {
                    StartCoroutine(WaitUntilGrounded());
                }
            }
        }

        //jump
        if (isPlayerTwo == false)
        {
            if (Input.GetButtonDown("Jump") && OnGround)
            {
                Jump();

            }

            if (Input.GetButtonDown("Jump") && !OnGround && DoubleJumpAvailable)
            {
                DoubleJumpAvailable = false;
                Jump();
            }
        }
        else
        {
            if (Input.GetButtonDown("P2Jump") && OnGround)
            {
                Jump();
            }

            if (Input.GetButtonDown("P2Jump") && !OnGround && DoubleJumpAvailable)
            {
                DoubleJumpAvailable = false;
                Jump();
            }
        }

        // Toggles

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseDisplay();
        }

        //birds eye cam

        if (SceneManager.GetActiveScene().name != "TwoPlayer")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (Cam.enabled == false)
                {
                    Cam.enabled = true;
                    bec.birdseyecam.enabled = false;
                }
                else if (Cam.enabled == true)
                {
                    Cam.enabled = false;
                    bec.birdseyecam.enabled = true;
                }
            }
        }

    }

    //gameplay functions

    IEnumerator WaitUntilGrounded()
    {
        if (OnGround == true && isSprinting == false)
        {
            Changed = false;
            isSprinting = true;
            yield return null;
        }

        if (OnGround == true && isSprinting == true)
        {
            Changed = false;
            isSprinting = false;
            yield return null;
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        PlayJumpSound();
    }

    //toggles

    public void PauseDisplay()
    {
        if (PauseObjects.activeInHierarchy)
        {
            DiceCam.GetComponent<RawImage>().enabled = true;
            if(AccessibilityObjects.activeInHierarchy == true)
            {
                AccessibilityObjects.SetActive(false);
            }
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            PauseObjects.SetActive(false);
        }
        else
        {
            DiceCam.GetComponent<RawImage>().enabled = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            PauseObjects.SetActive(true);
        }
    }

    //Accessibility

    public void GetCameraShake(bool value)
    {
        CamShakeEnabled = value;
    }

    //SFX

    private void PlayJumpSound()
    {
        SFXAudioSource.PlayOneShot(JumpSFX, volume);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            CanHitDice = true;
        }
        if(other.CompareTag("BoardPiece"))
        {
            CurrentlyOnSquare = other.gameObject;
            CheckMine = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            CanHitDice = false;
        }
        if (other.CompareTag("BoardPiece"))
        {
            CheckMine = false;
        }
    }
}
