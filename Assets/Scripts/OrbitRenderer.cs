using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRenderer : MonoBehaviour
{
    LineRenderer lr;
    public ControlPlayer control;
    PlanetGravity[] planetGravs;

    public Rigidbody2D rb;

    public List<Vector3> predictedPoints = new List<Vector3>();
    public List<Vector3> linePoints = new List<Vector3>();

    public float timeStep;
    public float timeMod;
    private float finalStep;
    public float resolution = 1;
    public int length = 100;

    Vector2 position2D;
    Vector2 computedVelocity;
    Vector2 netAccel;
    Vector2 direction;
    Vector2 pastPoint;
    Vector2 nextPoint;
    float distance;
    float calcLength;
    float lrLength;
    List<Vector2> computedForce = new List<Vector2>();

    public List<Vector3> timeAfterWarp = new List<Vector3>();

    public float correction;

    float mass;

    private LineRenderer timeLine;

    private float timeNoWarp;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        timeStep = Time.fixedDeltaTime;
        planetGravs = FindObjectsOfType<PlanetGravity>();
        mass = rb.mass;

        lr.positionCount = (int)(length / resolution) + 1;
        timeLine = GameObject.Find("TimeLine").GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (control.releaseSpeed != new Vector2(0, 0))
        {
            lr.SetPositions(linePoints.ToArray());
        }
    }

    void FixedUpdate()
    {
        predictedPoints.Clear();
        linePoints.Clear();
        computedForce.Clear();
        position2D = new Vector2(rb.transform.position.x, rb.transform.position.y);

        netAccel = new Vector2(0, 0);

        for (int i = 0; i < planetGravs.Length; i++)
        {
            direction = AngleBetweenPoints(planetGravs[i].position, rb.transform.position);
            distance = Vector2.Distance(planetGravs[i].position, rb.transform.position);
            computedForce.Add(planetGravs[i].forceComponentConstant * direction / (distance * distance));
            netAccel += computedForce[i] / mass;
        }

        //Calculate time warp around large celestial bodies.
        if (control.released)
        {
            control.timeSinceStart -= Mathf.Pow(Mathf.Clamp(netAccel.magnitude - 20f, 0, 100000), 0.33f) / 10f;
            //Uncomment for time graph.
            timeNoWarp += Time.deltaTime;
            timeLine.positionCount = timeAfterWarp.Count;
            timeAfterWarp.Add(new Vector3(timeNoWarp, control.timeSinceStart, 0));
            timeLine.SetPositions(timeAfterWarp.ToArray());
        }
        
        if (control.released)
        {
            finalStep = timeStep / rb.velocity.magnitude * timeMod;
            computedVelocity = rb.velocity + netAccel * finalStep;
        }
        else
        {
            finalStep = timeStep / control.releaseSpeed.magnitude * timeMod;
            computedVelocity = control.releaseSpeed + netAccel * finalStep;
        }

        predictedPoints.Add(position2D + (computedVelocity * finalStep));
        linePoints.Add(position2D);
        linePoints.Add(position2D + (computedVelocity * finalStep));

        calcLength = (computedVelocity * finalStep).magnitude;

        for (int u = 0; u < length; u++)
        {
            finalStep = timeStep / computedVelocity.magnitude * timeMod;

            pastPoint = predictedPoints[u];
            nextPoint = pastPoint + (computedVelocity * finalStep / (1 + (u * correction * timeMod)));

            netAccel = new Vector2(0, 0);
            computedForce.Clear();
            for (int i = 0; i < planetGravs.Length; i++)
            {
                direction = AngleBetweenPoints(planetGravs[i].position, pastPoint);
                distance = Vector2.Distance(planetGravs[i].position, pastPoint);
                if (distance < planetGravs[i].transform.localScale.x)
                {
                    for (int v = 0; v < (length - u) / resolution; v++)
                    {
                        linePoints.Add(new Vector3(Mathf.Clamp(nextPoint.x, transform.parent.position.x - 1000, transform.parent.position.x + 1000), Mathf.Clamp(nextPoint.y, transform.parent.position.y - 1000, transform.parent.position.y + 1000), 0));
                    }
                    u = length;
                }
                computedForce.Add(planetGravs[i].forceComponentConstant * direction / (distance * distance));
                netAccel += computedForce[i] / mass;
            }

            computedVelocity += netAccel * finalStep;

            predictedPoints.Add(new Vector3(nextPoint.x, nextPoint.y, 0));
            if (u % resolution == 0)
            {
                linePoints.Add(new Vector3(Mathf.Clamp(nextPoint.x, transform.parent.position.x - 1000, transform.parent.position.x + 1000), Mathf.Clamp(nextPoint.y, transform.parent.position.y - 1000, transform.parent.position.y + 1000), 0));
                calcLength += Vector2.Distance(linePoints[linePoints.Count - 1], linePoints[linePoints.Count - 2]);
            }
        }

        lrLength = calcLength;

    }

    Vector2 AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y).normalized;
    }
}
