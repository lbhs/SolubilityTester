using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFillCameraViewport : MonoBehaviour
{
    private float oldAspect;

    // Start is called before the first frame update
    void Start()
    {
        oldAspect = Camera.main.aspect;
        GetComponent<SpriteRenderer>().sortingOrder = -32768;
        ResizeSpriteToScreen();
    }

    private void ResizeSpriteToScreen()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float width = spriteRenderer.sprite.bounds.size.x;
        float height = spriteRenderer.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / (height));
    }

    private void Update()
    {
        if (Camera.main.aspect != oldAspect)
        {
            ResizeSpriteToScreen();
            oldAspect = Camera.main.aspect;
        }
    }
}
