using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCompressionController : MonoBehaviour
{
    [Header("Background")]
    [SerializeField]
    private float backgroundBottomPosition;

    [SerializeField]
    private float backgroundTopPosition;

    [SerializeField]
    private Button controlButton;

    private float targetYPosition;

    private bool atBottom = false;

    public bool AtBottom
    {
        get
        {
            return atBottom;
        }
    }

    public void MoveWaterlineDown()
    {
        if (!atBottom)
        {
            targetYPosition = backgroundBottomPosition;
            controlButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Move Waterline Up";
        }
        else
        {
            targetYPosition = backgroundTopPosition;
            controlButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Move Waterline Down";
        }
        StartCoroutine(MoveWaterlineCoroutine());
        atBottom = !atBottom;
    }

    private IEnumerator MoveWaterlineCoroutine()
    {
        controlButton.interactable = false;

        while (transform.position.y != targetYPosition)
        {
            // Background (this object)
            Vector3 perFrameDesiredLocationImage = transform.position;
            perFrameDesiredLocationImage.y = Mathf.MoveTowards(transform.position.y,
                                                               targetYPosition,
                                                               1.5f * Time.fixedDeltaTime);
            GetComponent<Rigidbody>().MovePosition(perFrameDesiredLocationImage);

            //// waterLine (has 2D collider to intercept collisions with the salt sprites)
            //Vector3 perFrameDesiredLocationWaterline = waterLine.transform.position;
            //perFrameDesiredLocationWaterline.y = Mathf.MoveTowards(waterLine.transform.position.y,
            //                                              targetYPosition,
            //                                              1.5f * Time.fixedDeltaTime);
            //waterLine.transform.position = perFrameDesiredLocationWaterline;
            
            //// topWall (prevents 3D ions from escaping the bounds)
            //Vector3 perFrameDesiredLocation = topWall.transform.position;
            //perFrameDesiredLocation.y = Mathf.MoveTowards(topWall.transform.position.y,
            //                                              targetYPosition,
            //                                              1.5f * Time.fixedDeltaTime);
            //topWall.GetComponent<Rigidbody>().MovePosition(perFrameDesiredLocation);

            yield return new WaitForFixedUpdate();
        }

        controlButton.interactable = true;
    }
}
