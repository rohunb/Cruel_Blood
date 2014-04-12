using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{


    public GUITexture gameStart;
    public bool isStartGameButton;
    GUITexture playGameTexture;
    // Use this for initialization
    void Awake()
    {
        playGameTexture = GetComponent<GUITexture>();
    }
    void Start()
    {
        playGameTexture.transform.position = Vector3.zero;
        playGameTexture.pixelInset = new Rect((Screen.width / 2f) - (Screen.width / (4.3f * 2)), (Screen.height / 2f) - (Screen.height / (4.3f * 2f))- Screen.height/12.0f, Screen.width / 4.3f, Screen.height / 4.3f);
        //howToPlayTexture.pixelInset = new Rect(-136, -40, Screen.width / 4.3f, Screen.height / 4.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        if (isStartGameButton)
        {
            StatsAndGlobals.Instance.ResetStats();
            Application.LoadLevel("Scene1");
        }
    }
}
