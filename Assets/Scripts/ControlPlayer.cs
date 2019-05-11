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
            Touch touch = Input.touches[0];
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
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        else
        {
            left = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        else
        {
            right = false;
        }

        if (Input.GetMouseButtonDown(0) && !released)
        {
            followGO = Instantiate(mouseFollow, transform.position, Quaternion.identity);
            followGO.GetComponent<SetVelocity>().playerT = transform;
            followGO.GetComponent<SetVelocity>().control = this;
            followGO.GetComponent<SpriteRenderer>().color = GetComponent<Colourise>().color;
            followGO.GetComponent<LineRenderer>().startColor = GetComponent<Colourise>().color;
            followGO.GetComponent<LineRenderer>().endColor = GetComponent<Colourise>().color;
        }

        if (Input.GetMouseButtonUp(0) && !released)
        {
            Release(releaseSpeed);
            Destroy(followGO);
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
            r.constraints = RigidbodyConstraints2D.FreezeAll;
            Vector3 originalPos = transform.position;
            for (int i = 0; i < 100; i++)
            {
                transform.position = Vector2.Lerp(originalPos, collision.transform.position, i / 100f);
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 60; i++)
            {
                transform.localScale = transform.localScale * 1.1f;
                yield return new WaitForEndOfFrame();
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
            boost.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boostAmount * 3f);
        }
        else if (right && !left && released && boostAmount > 0)
        {
            r.AddForce(Quaternion.Euler(new Vector3(0, 0, -90)) * r.velocity.normalized * controlForce);
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = false;
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = true;
            boostAmount -= 0.5f;
            boost.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boostAmount * 3f);
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
