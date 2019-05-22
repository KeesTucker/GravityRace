using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Analytics;

public class ControlPlayer : MonoBehaviour
{
    public Rigidbody2D r;

    public bool left;
    public bool right;

    public bool released;

    public Vector2 releaseSpeed;

    public float controlForce;

    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;

    public GameObject mouseFollow;
    private GameObject followGO;
    public GameObject explode;

    public float timeSinceStart;
    public TMPro.TMP_Text score;

    public LevelManager levelManager;

    private GameObject endScreen;
    private GameObject runScreen;

    private RectTransform boost;

    public float boostAmount = 100f;

    private SmoothFollow cam;

    public bool stop = false;

    public Transform sun;

    public GameObject particle;

    public Sprite end;

    private SoundFX soundFX;

    void Start()
    {
        r.isKinematic = true;
        released = false;

        score = GameObject.Find("Score").GetComponent<TMPro.TMP_Text>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        endScreen = GameObject.Find("Ended");
        GameObject.Find("Level").GetComponent<TMPro.TMP_Text>().text = levelManager.levelNumber.ToString();
        endScreen.SetActive(false);
        runScreen = GameObject.Find("Running");

        boost = GameObject.Find("Boost").GetComponent<RectTransform>();
        cam = FindObjectOfType<SmoothFollow>();

        sun = GameObject.Find("End").transform;

        soundFX = GetComponent<SoundFX>();
    }

    public void Release(Vector2 velocity)
    {
        r.isKinematic = false;
        r.velocity = velocity;
        released = true;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                left = true;
                right = false;
                
            }
            else if (touch.position.x > Screen.width / 2)
            {
                left = false;
                right = true;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                left = true;
                right = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                right = true;
                left = false;
            }
            else
            {
                right = false;
                left = false;
            }
        }
        

        if (Input.GetMouseButtonDown(0) && !released && !stop)
        {
            followGO = Instantiate(mouseFollow, transform.position, Quaternion.identity);
            followGO.GetComponent<SetVelocity>().playerT = transform;
            followGO.GetComponent<SetVelocity>().control = this;
            followGO.GetComponent<SpriteRenderer>().color = GetComponent<Colourise>().color;
            followGO.GetComponent<LineRenderer>().startColor = GetComponent<Colourise>().color;
            followGO.GetComponent<LineRenderer>().endColor = GetComponent<Colourise>().color;
        }

        if (followGO && Input.touchCount > 1)
        {
            Destroy(followGO);
        }

        if (Input.GetMouseButtonUp(0) && !released && followGO && !stop)
        {
            Release(releaseSpeed);
            Destroy(followGO);
            cam.Release();
        }

        if (released)
        {
            timeSinceStart += Time.deltaTime;
            score.text = timeSinceStart.ToString("#.#") + "s";
        }
        else if (!stop)
        {
            transform.up = releaseSpeed.normalized;
        }
        else
        {
            transform.up = AngleBetweenPoints(sun.position, transform.position);
        }
    }

    Vector2 AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y).normalized;
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "planet" || collision.collider.tag == "dynamic") //Retry run.
        {
            soundFX.Death();
            explode.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            r.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(1f);
            FindObjectOfType<SceneTransition>().SceneTrans(SceneManager.GetActiveScene().name);
            /*if (Random.Range(0, 2) == 1)
            {
                if (PlayerPrefs.HasKey("AdConfig"))
                {
                    if (PlayerPrefs.GetInt("AdConfig") == 0)
                    {
                        FindObjectOfType<ShowAds>().GameOver();
                        Debug.Log("Ad Fired");
                    }
                }
                else
                {
                    FindObjectOfType<ShowAds>().GameOver();
                    Debug.Log("Ad Fired");
                }
            }*/
            SendFPS();
            FindObjectOfType<PlayGames>().Death();
        }
        else if (collision.collider.tag == "end") //Finish the run.
        {
            soundFX.Win();
            particle.SetActive(true);
            SendFPS();
            GetComponent<Collider2D>().enabled = false;
            released = false;
            stop = true;
            r.constraints = RigidbodyConstraints2D.FreezeAll;
            Vector3 originalPos = transform.position;
            for (int i = 0; i < 50; i++)
            {
                transform.position = Vector2.Lerp(originalPos, collision.transform.position, i / 50f);
                yield return new WaitForEndOfFrame();
            }
            GetComponent<SpriteRenderer>().sprite = end;
            GetComponent<SpriteRenderer>().color = Color.white;
            for (int i = 0; i < 8; i++)
            {
                transform.localScale = transform.localScale * 1.6f;
                yield return new WaitForEndOfFrame();
            }

            if (levelManager.levelNumber % 5 == 0)
            {
                if (PlayerPrefs.HasKey("AdConfig"))
                {
                    if (PlayerPrefs.GetInt("AdConfig") == 0)
                    {
                        FindObjectOfType<ShowAds>().GameOver();
                        Debug.Log("Ad Fired");
                    }
                }
                else
                {
                    FindObjectOfType<ShowAds>().GameOver();
                    Debug.Log("Ad Fired");
                }
            }

            if (timeSinceStart < levelManager.starTimes[2] && timeSinceStart > levelManager.starTimes[1])
            {
                FindObjectOfType<PlayGames>().GameComplete(1);
                if (PlayerPrefs.HasKey("LevelTime" + levelManager.levelNumber.ToString()))
                {
                    if (PlayerPrefs.GetFloat("LevelTime" + levelManager.levelNumber.ToString()) > timeSinceStart)
                    {
                        if (PlayerPrefs.HasKey("Stars"))
                        {
                            if (PlayerPrefs.HasKey("Level" + levelManager.levelNumber.ToString()))
                            {
                                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 1 - PlayerPrefs.GetInt("Level" + levelManager.levelNumber.ToString()));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 1);
                            }
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Stars", 1);
                        }
                        PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 1);
                        PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 1);
                    PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    if (PlayerPrefs.HasKey("Stars"))
                    {
                        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Stars", 1);
                    }
                }
                endScreen.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else if (timeSinceStart < levelManager.starTimes[1] && timeSinceStart > levelManager.starTimes[0])
            {
                FindObjectOfType<PlayGames>().GameComplete(2);
                if (PlayerPrefs.HasKey("LevelTime" + levelManager.levelNumber.ToString()))
                {
                    if (PlayerPrefs.GetFloat("LevelTime" + levelManager.levelNumber.ToString()) > timeSinceStart)
                    { 
                        if (PlayerPrefs.HasKey("Stars"))
                        {
                            if (PlayerPrefs.HasKey("Level" + levelManager.levelNumber.ToString()))
                            {
                                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 2 - PlayerPrefs.GetInt("Level" + levelManager.levelNumber.ToString()));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 2);
                            }
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Stars", 2);
                        }
                        PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 2);
                        PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 2);
                    PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    if (PlayerPrefs.HasKey("Stars"))
                    {
                        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 2);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Stars", 2);
                    }
                }
                endScreen.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                endScreen.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            }
            else if (timeSinceStart < levelManager.starTimes[0])
            {
                FindObjectOfType<PlayGames>().GameComplete(3);
                if (PlayerPrefs.HasKey("LevelTime" + levelManager.levelNumber.ToString()))
                {
                    if (PlayerPrefs.GetFloat("LevelTime" + levelManager.levelNumber.ToString()) > timeSinceStart)
                    {
                        if (PlayerPrefs.HasKey("Stars"))
                        {
                            if (PlayerPrefs.HasKey("Level" + levelManager.levelNumber.ToString()))
                            {
                                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 3 - PlayerPrefs.GetInt("Level" + levelManager.levelNumber.ToString()));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 3);
                            }
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Stars", 3);
                        }
                        PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 3);
                        PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 3);
                    PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    if (PlayerPrefs.HasKey("Stars"))
                    {
                        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 3);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Stars", 3);
                    }
                }
                endScreen.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                endScreen.transform.GetChild(1).GetComponent<Image>().color = Color.white;
                endScreen.transform.GetChild(2).GetComponent<Image>().color = Color.white;
            }
            else
            {
                runScreen.SetActive(false);
                endScreen.SetActive(true);
                Destroy(GameObject.Find("Next"));
            }

            runScreen.SetActive(false);
            endScreen.SetActive(true);
        }
    }

    private void SendFPS()
    {
        int averageFPS = transform.GetChild(2).GetComponent<OrbitRenderer>().AverageFPS();

        Debug.Log(averageFPS);

        Parameter FPS = new Parameter("average_FPS", averageFPS);

        FirebaseAnalytics.LogEvent("level_FPS_Average", FPS);
    }

    void FixedUpdate()
    {
        if (left && !right && released && boostAmount > 0)
        {
            r.AddForce(Quaternion.Euler(new Vector3(0, 0, 90)) * r.velocity.normalized * controlForce);
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = false;
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = true;
            boostAmount -= 0.5f;
            boost.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boostAmount * 4.11f);
            StartCoroutine(soundFX.RocketOn());
        }
        else if (right && !left && released && boostAmount > 0)
        {
            r.AddForce(Quaternion.Euler(new Vector3(0, 0, -90)) * r.velocity.normalized * controlForce);
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = false;
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = true;
            boostAmount -= 0.5f;
            boost.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boostAmount * 4.11f);
            StartCoroutine(soundFX.RocketOn());
        }
        else
        {
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = false;
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = false;
            StartCoroutine(soundFX.RocketOff());
        }
    }
}
