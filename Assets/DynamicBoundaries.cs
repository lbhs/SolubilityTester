using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBoundaries : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    private float oldAspect;

    // Start is called before the first frame update
    private void Start()
    {
        oldAspect = Camera.main.aspect;
        CalculateBounds();
    }

    private void Update()
    {
        if (Camera.main.aspect != oldAspect)
        {
            CalculateBounds();
            oldAspect = Camera.main.aspect;
        }
    }

    private void CalculateBounds()
    {
        Vector3 leftMostPosition = Camera.main.ViewportToWorldPoint(new Vector3(.0f, .5f, -Camera.main.transform.position.z));
        leftWall.transform.position = leftMostPosition;

        Vector3 rightMostPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, .5f, -Camera.main.transform.position.z));
        rightWall.transform.position = rightMostPosition;
    }
}
