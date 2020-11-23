using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionImageController : MonoBehaviour
{
    private Image reactionImage;

    // Start is called before the first frame update
    void Start()
    {
        reactionImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadReactionImage(string metalGameObjectName, string ionGameObjectName)
    {
        // metalName should just contain the name of the metal (e.g. Cu)
        string metalName = metalGameObjectName.Split(' ')[1].Substring(0, 2);
        // ionName should just contain the name of the ion that collided with the metal (e.g. Ag+)
        string ionName = ionGameObjectName.Split(' ')[1].Substring(0, 3);

        // Asset path relative to the Assets/Resources folder
        string fileName = string.Format("ReactionImages/{0},{1}", metalName, ionName);
        Debug.Log(fileName);
        Sprite imageSprite = Resources.Load<Sprite>(fileName);
        if (imageSprite != null)
            reactionImage.enabled = true;
        else
            reactionImage.enabled = false;
        GetComponent<Image>().sprite = imageSprite;
    }
}
