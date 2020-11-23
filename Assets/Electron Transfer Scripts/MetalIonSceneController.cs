using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class MetalIonSceneController : MonoBehaviour
{
    // Metal Selector
    [SerializeField]
    private Dropdown chooseMetal;

    // Salt Selector
    [SerializeField]
    private Dropdown chooseSalt;

    // Scene Controller - main display
    [SerializeField]
    private Text display;

    // VideoPlayer component that plays reaction videos
    [SerializeField]
    private VideoPlayer reactionVidPlayer;

    // It is repetitive to play reaction videos after multiple collisions
    // Let the user choose if they want to play the video again by making the button appear
    [SerializeField]
    private GameObject playVideoAgainButton;

    public static MetalIonSceneController Instance;

    // REFACTOR: this is already tracked by the dispenser
    private int numMetalChosen;

    private bool videoHasBeenPlayed;

    [SerializeField]
    private List<string> reactionVidNames;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton design pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        display.text = "Create three metal layers.";
        // Don't display salt selector yet
        chooseSalt.transform.parent.gameObject.SetActive(false);
        videoHasBeenPlayed = false;
    }

    public void MetalChosen()
    {
        if (numMetalChosen == 2)
        {
            display.text = "Choose a salt.";
            // Display salt selector (and hide metal selector)
            chooseMetal.transform.parent.gameObject.SetActive(false);
            chooseSalt.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            numMetalChosen++;
            display.text = string.Format("You have created {0} layers.", numMetalChosen);
        }
    }

    public void LaunchReactionVideo(string metalGameObjectName, string ionGameObjectName)
    {
        if (!videoHasBeenPlayed)
        {
            // metalName should just contain the name of the metal (e.g. Cu)
            string metalName = metalGameObjectName.Split(' ')[1].Substring(0, 2);
            // ionName should just contain the name of the ion that collided with the metal (e.g. Ag+)
            string ionName = ionGameObjectName.Split(' ')[1].Substring(0, 3);

            string fileName = string.Format("{0},{1}.mov", metalName, ionName);
            if (reactionVidNames.Contains(fileName))
            {
                string completeUrl;
#if UNITY_WEBGL
                completeUrl = "https://lbhs.github.io/Games/" + "IonicReactionSimulatorDev" + "/StreamingAssets/" + fileName;
#else
                completeUrl = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
#endif
                reactionVidPlayer.url = completeUrl;
                reactionVidPlayer.gameObject.transform.parent.gameObject.SetActive(true);

                videoHasBeenPlayed = true;
                playVideoAgainButton.SetActive(true);
            }
        }
    }
}
