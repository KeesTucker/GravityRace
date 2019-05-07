using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        r.isKinematic = true;
        released = false;
    }

    public void Release(Vector2 velocity)
    {
        r.isKinematic = false;
        r.velocity = velocity;
        released = true;
    }

    void Update()
    {
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
    }

    void FixedUpdate()
    {
        if (left && !right && released)
        {
            r.AddForce(Quaternion.Euler(new Vector3(0, 0, 90)) * r.velocity.normalized * controlForce);
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = false;
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = true;
        }
        else if (right && !left && released)
        {
            r.AddForce(Quaternion.Euler(new Vector3(0, 0, -90)) * r.velocity.normalized * controlForce);
            ParticleSystem.EmissionModule emissionL = leftParticle.GetComponent<ParticleSystem>().emission;
            emissionL.enabled = false;
            ParticleSystem.EmissionModule emissionR = rightParticle.GetComponent<ParticleSystem>().emission;
            emissionR.enabled = true;
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
