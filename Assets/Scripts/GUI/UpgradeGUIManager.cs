using UnityEngine;
using System.Collections;

public class UpgradeGUIManager : MonoBehaviour {
    public Texture[] MainButtonTextures;
    public Texture barTexture;
    public Texture barTransparent;
    public GameObject[] buttons;
    public GameObject[,] bars;
    public int numBars;
    Rect[] touchZones;
    public Vector3 firstButtonPos;
    public float buttonWidth;
    public float buttonPadding;
    public float buttonHeight;
    private Rect buttonRect;
    private Rect barRect;
    public float barPadding;
    private int[] upgradeLevel;
    public Texture[] confBoxTextures;
    public GameObject[] confBoxes;
    public Vector3 confBoxStartPos;
    public float confBoxWidth;
    public float confBoxHeight;
    private bool showConf;
    private string upgradeText;
    private int upgradeLevelForText;
    private bool isUpgrading;
	// Use this for initialization
	void Start () {

        buttonWidth = Screen.width / 21.333f;
        buttonHeight = Screen.height / 13.0f;
        buttonPadding = Screen.width/13.44f;

        confBoxWidth = Screen.width / 3.77f;
        confBoxHeight = Screen.height / 7.2f;

        touchZones = new Rect[buttons.Length+confBoxes.Length];
        showConf = false;
        //buttonPadding = Screen.width / (buttons.Length*2.4f);
        firstButtonPos.x += buttonPadding;
        upgradeLevel = new int[buttons.Length];
        bars = new GameObject[buttons.Length, numBars];
        guiTextChanger.textToWrite = "";
        for (int i = 0; i < confBoxes.Length; i++)
        {
            confBoxes[i] = new GameObject("ConfBoxes");
            confBoxes[i].AddComponent<GUITexture>();
            confBoxes[i].transform.position = Vector3.zero;
            confBoxes[i].transform.localScale = Vector3.zero;
            //confBoxes[i].guiTexture.texture=confBoxTextures[i];
        }



        confBoxes[0].transform.position = new Vector3(0f, 0f, 1f);
        confBoxes[1].transform.position = new Vector3(0f, 0f, 1f);
        confBoxes[2].transform.position = new Vector3(0f, 0f, 1f);
        //yes button

        //buttonRect = new Rect(Screen.width / 2 - buttonPadding * 2, Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight);
        //buttonRect = new Rect(Screen.width / 2 - confBoxWidth / 2 , Screen.height / 2 - (confBoxHeight*2), buttonWidth, buttonHeight);
        buttonRect = new Rect(Screen.width / 2.50f, Screen.height / 1.86f, buttonWidth, buttonHeight);
        confBoxes[1].guiTexture.pixelInset = buttonRect;
        //touchZones[buttons.Length + 1] = new Rect(buttonRect);
        touchZones[buttons.Length + 1] = new Rect(buttonRect);

        //no button
        //buttonRect = new Rect(433.1f,
        //                    182.96f,
        //                    buttonWidth, buttonHeight);
        //buttonRect = new Rect(Screen.width / 2 + buttonPadding, Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight);
        //buttonRect = new Rect(Screen.width / 2 + confBoxWidth / 2 - buttonWidth * 2, Screen.height / 2 - (confBoxHeight * 2), buttonWidth, buttonHeight);
        buttonRect = new Rect(Screen.width / 2.01f, Screen.height / 1.86f, buttonWidth, buttonHeight);
        confBoxes[2].guiTexture.pixelInset = buttonRect;
        touchZones[buttons.Length + 2] = new Rect(buttonRect);

        //dialogue box
        buttonRect = new Rect(Screen.width / 2 - confBoxWidth / 2, Screen.height / 2 + (confBoxHeight), confBoxWidth, confBoxHeight);
        confBoxes[0].guiTexture.pixelInset = buttonRect;


        touchZones[buttons.Length] = new Rect(buttonRect);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = new GameObject();
            upgradeLevel[i] = 1;
            buttonRect = new Rect(firstButtonPos.x + i * (buttonWidth + buttonPadding),
                firstButtonPos.y ,
            buttonWidth, buttonHeight);
            buttons[i].AddComponent<GUITexture>();
            buttons[i].guiTexture.texture = MainButtonTextures[i];
            buttons[i].guiTexture.pixelInset = buttonRect;
            buttons[i].transform.position = Vector3.zero;
            buttons[i].transform.localScale = Vector3.zero;
            touchZones[i] = new Rect(buttonRect);
            
            for (int j = 0; j < numBars; j++)
            {
                barRect = new Rect(buttonRect);
                barRect.y += (j + 1) * buttonHeight + barPadding;
                bars[i, j] = new GameObject();
                bars[i, j].AddComponent<GUITexture>();
                bars[i, j].guiTexture.texture = barTransparent;
                bars[i, j].guiTexture.pixelInset = barRect;
                bars[i, j].transform.position = Vector3.zero;
                bars[i, j].transform.localScale = Vector3.zero;
                
            }

        }
        //Debug.Log(StatsAndGlobals.Instance.swordLevel);
        upgradeLevel[0] = StatsAndGlobals.Instance.swordLevel;
        upgradeLevel[1] = StatsAndGlobals.Instance.hammerLevel;
        upgradeLevel[2] = StatsAndGlobals.Instance.bowLevel;
        upgradeLevel[3] = StatsAndGlobals.Instance.spearLevel;
        upgradeLevel[4] = StatsAndGlobals.Instance.axeLevel;
        upgradeLevel[5] = StatsAndGlobals.Instance.armourLevel;
        upgradeLevel[6] = StatsAndGlobals.Instance.speedLevel;
        //yes button
        //buttonRect = new Rect(322.23f,182.96f, buttonWidth, buttonHeight);
        
       


        
	}
	bool GetConfirmation(string upgradeText , int upgradeLevel)
    {
        if(!isUpgrading)
        {
            guiTextChanger.textToWrite = "Sorry, you need " + ((upgradeLevel + 1) * 100) + " Blood!";
        }
        else
        {
            guiTextChanger.textToWrite = "Buy " + upgradeText + " upgrade for " + ((upgradeLevel + 1) * 100) + " Blood?";
        }
        
        int count=Input.touchCount;
        bool yesButtonTouched = false;
        bool noButtonTouched = false;
        
            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touchZones[buttons.Length + 1].Contains(touch.position))
                {
                    yesButtonTouched = true;
                    
                    
                }
                else if (touchZones[buttons.Length + 2].Contains(touch.position))
                {
                    noButtonTouched = true;
                   
                    
                }
                
            }
        //debug mouse
            if (Input.GetMouseButtonUp(0))
            {
                if (touchZones[buttons.Length + 1].Contains(Input.mousePosition))
                {
                    yesButtonTouched = true;
                   
                }
                else if (touchZones[buttons.Length + 2].Contains(Input.mousePosition))
                {
                    noButtonTouched = true;
                    
                }
                
               
            }
        //debug mouse
            //if (isUpgrading)
            
                if (yesButtonTouched)
                {
                    showConf = false;
                    return true;
                }
                else if (noButtonTouched)
                {
                    showConf = false;
                    return false;
                }
                else
                    return false;
            
            //else 
            //{
            //    if (yesButtonTouched)
            //    {
            //        showConf = false;
            //        //isUpgrading = false;
            //        return true;
            //    }
            //    else if (noButtonTouched)
            //    {
            //        showConf = false;
            //        //isUpgrading = false;
            //        return false;
            //    }
            //    else
                
            //        return false;
            //}
        
        
        
        
    }

	// Update is called once per frame
	void Update () {
        if (!showConf)
            guiTextChanger.textToWrite = "";
        for (int i = 0; i < confBoxes.Length; i++)
        {
            if (showConf)
                confBoxes[i].guiTexture.texture = confBoxTextures[i];
            else
                confBoxes[i].guiTexture.texture = null;


        }
        
            if (showConf )
            {
                if (isUpgrading && GetConfirmation(upgradeText, upgradeLevel[upgradeLevelForText]))
                {
                    StatsAndGlobals.Instance.ReduceBlood((upgradeLevel[upgradeLevelForText] + 1) * 100);
                    upgradeLevel[upgradeLevelForText]++;
                    switch (upgradeLevelForText)
                    {
                        case 0:
                            //sword
                            
                            StatsAndGlobals.Instance.swordLevel = upgradeLevel[upgradeLevelForText];
                            
                            break;
                        case 1:
                            StatsAndGlobals.Instance.hammerLevel = upgradeLevel[upgradeLevelForText];
                            break;
                        case 2:
                            StatsAndGlobals.Instance.bowLevel = upgradeLevel[upgradeLevelForText];
                            break;
                        case 3:
                            StatsAndGlobals.Instance.spearLevel = upgradeLevel[upgradeLevelForText];
                            break;
                        case 4:
                            StatsAndGlobals.Instance.axeLevel = upgradeLevel[upgradeLevelForText];
                            break;
                        case 5:
                            StatsAndGlobals.Instance.armourLevel = upgradeLevel[upgradeLevelForText];
                            break;
                        case 6:
                            StatsAndGlobals.Instance.speedLevel = upgradeLevel[upgradeLevelForText];
                            break;
                        default:
                            break;

                    }

                }
                else
                {
                    GetConfirmation(upgradeText, upgradeLevel[upgradeLevelForText]);
                }
            
            }
            
        
           
        
        int count = Input.touchCount;
        //Debug.Log(StatsAndGlobals.Instance.CurrentLitresOfBlood);
        //Debug.Log(upgradeLevel[0]);
        //Debug.Log("upgrade" + ((upgradeLevel[0] + 1) * 100));
        //Debug.Log((StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[0] + 1) * 100)));
        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);
            
            if (touchZones[0].Contains(touch.position) && upgradeLevel[0]<numBars )
            {
                showConf = true;
                upgradeLevelForText = 0;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[0] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Sword";
                    upgradeLevelForText = 0;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
                
            }
            else if (touchZones[1].Contains(touch.position) && upgradeLevel[1] < numBars)
            {
                showConf = true;

                upgradeLevelForText = 1;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[1] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Hammer";
                    upgradeLevelForText = 1;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
               
            }
            else if (touchZones[2].Contains(touch.position) && upgradeLevel[2] < numBars)
            {
                showConf = true;

                upgradeLevelForText = 2;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[2] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Bow";
                    upgradeLevelForText = 2;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[3].Contains(touch.position) && upgradeLevel[3] < numBars)
            {
                showConf = true;

                upgradeLevelForText = 3;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[3] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Spear";
                    upgradeLevelForText = 3;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[4].Contains(touch.position) && upgradeLevel[4] < numBars)
            {
                showConf = true;

                upgradeLevelForText = 4;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[4] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Axe";
                    upgradeLevelForText = 4;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[5].Contains(touch.position) && upgradeLevel[5] < numBars)
            {
                showConf = true;

                upgradeLevelForText = 5;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[5] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Armour";
                    upgradeLevelForText = 5;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[6].Contains(touch.position) && upgradeLevel[6] < numBars)
            {
                showConf = true;

                upgradeLevelForText = 6;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[6] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Speed";
                    upgradeLevelForText = 6;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            
        }
        //////debug mouse

        if (Input.GetMouseButtonUp(0))
        {
            if (touchZones[0].Contains(Input.mousePosition) && upgradeLevel[0] < numBars)
            {
                showConf = true;
                upgradeLevelForText = 0;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[0] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Sword";
                    upgradeLevelForText = 0;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                 }

            }
            else if (touchZones[1].Contains(Input.mousePosition) && upgradeLevel[1] < numBars)
            {
                showConf = true;
                
                upgradeLevelForText = 1;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[1] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Hammer";
                    upgradeLevelForText = 1;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
                
            }
            else if (touchZones[2].Contains(Input.mousePosition) && upgradeLevel[2] < numBars)
            {
                showConf = true;
                
                upgradeLevelForText = 2;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[2] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Bow";
                    upgradeLevelForText = 2;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[3].Contains(Input.mousePosition) && upgradeLevel[3] < numBars)
            {

                showConf = true;
                
                upgradeLevelForText = 3;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[3] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Spear";
                    upgradeLevelForText = 3;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[4].Contains(Input.mousePosition) && upgradeLevel[4] < numBars)
            {
                showConf = true;
              
                upgradeLevelForText = 4;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[4] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Axe";
                    upgradeLevelForText = 4;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[5].Contains(Input.mousePosition) && upgradeLevel[5] < numBars)
            {
                showConf = true;
                
                upgradeLevelForText = 5;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[5] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Armour";
                    upgradeLevelForText = 5;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
            else if (touchZones[6].Contains(Input.mousePosition) && upgradeLevel[6] < numBars)
            {
                showConf = true;
                
                upgradeLevelForText = 6;
                if (StatsAndGlobals.Instance.CurrentLitresOfBlood >= ((upgradeLevel[6] + 1) * 100))
                {
                    showConf = true;
                    upgradeText = "Speed";
                    upgradeLevelForText = 6;
                    isUpgrading = true;
                }
                else
                {
                    showConf = true;
                    isUpgrading = false;
                }
            }
        }
        ///////debug mouse
        
        for (int i = 0; i < upgradeLevel.Length; i++)
        {
            //Debug.Log(upgradeLevel[i]);
            for (int j = 0; j < upgradeLevel[i]; j++)
            {
                //if (!bars[i, j])
               //Debug.Log("null " + i + " j "+j);
                //bars[i, j].guiTexture.texture = barTexture;
               bars[i, j].guiTexture.texture = barTexture;
               
            }
        }
        
	}
}
