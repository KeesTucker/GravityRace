using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "planet" || collision.collider.tag == "dynamic") //Retry run.
        {
            explode.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            r.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.collider.tag == "end") //Finish the run.
        {
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
            for (int i = 0; i < 30; i++)
            {
                transform.localScale = transform.localScale * 2.2f;
                yield return new WaitForEndOfFrame();
            }

            if (levelManager.levelNumber % 5 == 0)
            {
                if (PlayerPrefs.HasKey("NoAds"))
                {
                    if (PlayerPrefs.GetInt("NoAds") == 0)
                    {
                        GetComponent<ShowAds>().GameOver();
                    }
                }
                else
                {
                    GetComponent<ShowAds>().GameOver();
                }
            }

            if (timeSinceStart < levelManager.starTimes[2] && timeSinceStart > levelManager.starTimes[1])
            {
                if (PlayerPrefs.HasKey("LevelTime" + levelManager.levelNumber.ToString()))
                {
                    if (PlayerPrefs.GetFloat("LevelTime" + levelManager.levelNumber.ToString()) > timeSinceStart)
                    {
                        PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 1);
                        PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 1);
                    PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                }
                endScreen.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else if (timeSinceStart < levelManager.starTimes[1] && timeSinceStart > levelManager.starTimes[0])
            {
                if (PlayerPrefs.HasKey("LevelTime" + levelManager.levelNumber.ToString()))
                {
                    if (PlayerPrefs.GetFloat("LevelTime" + levelManager.levelNumber.ToString()) > timeSinceStart)
                    {
                        PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 2);
                        PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 2);
                    PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                }
                endScreen.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                endScreen.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            }
            else if (timeSinceStart < levelManager.starTimes[0])
            {
                if (PlayerPrefs.HasKey("LevelTime" + levelManager.levelNumber.ToString()))
                {
                    if (PlayerPrefs.GetFloat("LevelTime" + levelManager.levelNumber.ToString()) > timeSinceStart)
                    {
                        PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 3);
                        PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("Level" + levelManager.levelNumber.ToString(), 3);
                    PlayerPrefs.SetFloat("LevelTime" + levelManager.levelNumber.ToString(), timeSinceStart);
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
        }
        else
        {
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = false;
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = false;
        }
    }
}
