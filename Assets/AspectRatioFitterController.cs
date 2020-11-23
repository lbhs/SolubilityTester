using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioFitterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Sprite imgSprite = GetComponent<Image>().sprite;
        //var ratio = imgSprite.rect.width / imgSprite.rect.height;
        GetComponent<AspectRatioFitter>().aspectRatio = Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
