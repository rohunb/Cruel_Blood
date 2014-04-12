using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public static GUIManager Instance;
    public Texture[] guiTextures;
    public GameObject[] buttons;
    Rect[] touchZones;
    public Vector3 firstButtonPos;
    public float buttonWidth;
    public float buttonHeight;
    public float buttonPadding;
    private Rect buttonRect;

    public PlayerMove Player;
    public Font ComboFont;

    private GUIText comboMeter;

    private int comboStreak;

    public int ComboStreak
    {
        get { return comboStreak; }
        private set { comboStreak = value; }
    }
    
    private bool animateCombo = false;
    private int amountScaled = 0;
    private bool shrink = false;

    public ParticleSystem comboExplosion;

    public ParticleSystem bloodSpray;

    public GUITexture healthIcon;
    private GameObject[] healthIcons;

	// Use this for initialization
	void Start () {

        buttonWidth = Screen.width / 21.33f;
        buttonHeight = Screen.height / 13.0f;
        buttonPadding = Screen.height / 9.75f;

        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        comboMeter = this.gameObject.AddComponent<GUIText>();
        comboMeter.fontSize =46;
        comboMeter.fontStyle = FontStyle.Bold;
        comboMeter.alignment = TextAlignment.Center;
        comboMeter.anchor = TextAnchor.MiddleCenter;
        comboMeter.color = Color.yellow;
       

        if (ComboFont != null)
        {
            comboMeter.font = ComboFont;
        }

        //Debug.Log("width " + Screen.height);
        buttonPadding = 1f;
        buttons[0] = new GameObject("Sword");
        buttons[1] = new GameObject("Hammer");
        buttons[2] = new GameObject("Bow");
        buttons[3] = new GameObject("Spear");
        buttons[4] = new GameObject("Axe");
        touchZones = new Rect[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonRect=new Rect(firstButtonPos.x,
                firstButtonPos.y+i*(buttonHeight+buttonPadding),
            buttonWidth,buttonHeight);
            //buttonRect = new Rect(-64, -64, 81, 81);
            buttons[i].AddComponent("GUITexture");
            buttons[i].guiTexture.texture = guiTextures[i];
            buttons[i].guiTexture.pixelInset = buttonRect;

            //buttons[i].transform.position = new Vector3(
            //    firstButtonPos.x + i * (buttonWidth + buttonPadding),
            //    firstButtonPos.y ,    firstButtonPos.z);
            buttons[i].transform.position = Vector3.zero;
            buttons[i].transform.localScale = Vector3.zero;
            touchZones[i] = new Rect(buttonRect);
        }

        healthIcons = new GameObject[Player.health];

        for (int i = 0; i < Player.health; i++)
        {
            healthIcons[i] =  new GameObject();
            GUITexture texture = healthIcons[i].AddComponent<GUITexture>();
            texture.guiTexture.texture = healthIcon.guiTexture.texture;
            texture.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            texture.transform.position = new Vector3(0.05f + (i * 0.05f), 0.95f, 0.0f);
        }
        
	}

    public void UpdateHealthDisplay(int currentHealth)
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i >= currentHealth)
                healthIcons[i].guiTexture.texture = null;
            else
                healthIcons[i].guiTexture.texture = healthIcon.texture;
        }
    }

    //void OnGUI()
    //{
        
    //}
    void Update()
    {
        int count = Input.touchCount;
        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);
            //for (int j = 0; j < touchZones.Length; j++)
            //{
            //    if(touchZones[j].Contains(touch.position))
            //    {

            //    }
            //}
            if(touchZones[0].Contains(touch.position))
            {
                guiTextChanger.textToWrite = "sword";
                Player.EquipWeapon("Sword");
            }
            else if(touchZones[1].Contains(touch.position))
            {
                guiTextChanger.textToWrite = "hammer";
                Player.EquipWeapon("Hammer");
            }
            else if (touchZones[2].Contains(touch.position))
            {
                guiTextChanger.textToWrite = "bow";
                Player.EquipWeapon("Bow");
            }
            else if (touchZones[3].Contains(touch.position))
            {
                guiTextChanger.textToWrite = "spear";
                Player.EquipWeapon("Spear");
            }
            else if (touchZones[4].Contains(touch.position))
            {
                guiTextChanger.textToWrite = "axe";
                Player.EquipWeapon("Axe");
            }
            else
                guiTextChanger.textToWrite = "none";
        }
        string comboToDisplay;
        if (comboStreak == 0)
            comboToDisplay = "00";
        else
            comboToDisplay = comboStreak.ToString();

        comboMeter.text = comboToDisplay;

        comboMeter.transform.position = new Vector3(0.95f, 0.5f, 0);

        if (animateCombo)
        {
            if (shrink)
            {
                comboMeter.fontSize -= 2;
                amountScaled -= 2;

                if (amountScaled <= 0)
                {
                    shrink = false;
                    amountScaled = 0;
                    animateCombo = false;
                }
            }
            else
            {
                comboMeter.fontSize += 2;
                amountScaled += 2;

                if (amountScaled >= 20)
                    shrink = true;
            }
        }
       
    }


    public void IncrementCombo(int numberToIncrease)
    {
        comboStreak += numberToIncrease;
        animateCombo = true;
    }

    public void ResetCombo()
    {
        comboStreak = 0;
        comboExplosion.transform.position =  Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0.5f, 10));
        comboExplosion.Play();
    }

    public void CreateBloodSplatter(Vector3 position)
    {
        ParticleSystem obj = (ParticleSystem)Instantiate(bloodSpray, this.transform.position, Quaternion.identity);
        obj.transform.position = position;
        obj.Play();
    }
	
}
