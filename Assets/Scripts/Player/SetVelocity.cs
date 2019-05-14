using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour
{
    public Transform playerT;
    public LineRenderer lr;
    public ControlPlayer control;
    public float velocityModifier = 4;

    void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, playerT.position);

        control.releaseSpeed = Vector2.Distance(transform.position, playerT.position) * velocityModifier * AngleBetweenPoints(playerT.position, transform.position);
    }

    Vector2 AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y).normalized;
    }
}
