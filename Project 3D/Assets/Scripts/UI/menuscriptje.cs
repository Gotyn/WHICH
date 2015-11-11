using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuScript : MonoBehaviour {
    //Singleton ------------------------

    private static GameObject menuInstance;
    public static MenuScript Instance
    {
        get
        {
            if (menuInstance == null)
            {
                menuInstance = Instantiate(Resources.Load("Menu"), Vector3.zero, Quaternion.identity) as GameObject;
            }
            return menuInstance.GetComponent<MenuScript>();
        }
    }


    //----------------------------------

    //Get a reference to the eventSystem so we can change the first selected button.
    EventSystem eventSystem;
 
    Camera camSpline;
    PlayerMovement sBroMovement, bBroMovement;
    Slider volumeSlider;

    Toggle dialogueToggle;

    public float splashTime = 3f;

    public Canvas mainMenuCanvas, 
                  exitMenuCanvas, 
                  pauseMenuCanvas, 
                  quitMenuCanvas,
                  splashCanvas,
                  optionsCanvas;
    private Canvas dialogueCanvas;

    //Needed to reference into EventSystem.
    public GameObject play, exit,               //mainMenu
                      exitYes, exitNo,          //exitGameMenu
                      cont, quit,               //pauseMenu
                      quitYes, quitNo;          //quitToMenu

    private Button playButton, exitButton,      //mainMenu
                   exitYesButton, exitNoButton, //exitGameMenu
                   continueButton, quitButton,  //pauseMenu
                   quitYesButton, quitNoButton; //quitToMenu

    public bool canvasOn, paused;

    public bool dialoguesEnabled; //Used to check whether the user disabled dialogues
    public bool dialoguesPlaying; //Used for the check whether an dialogue was playing before pausing.

    public float volume = 1;

    // Use this for initialization
    void Start() {

        dialogueCanvas = GameObject.Find("Chat").GetComponent<Canvas>();

        //Find references
        GetButtonReferences();

        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        dialogueToggle = GameObject.Find("DialoguesToggle").GetComponent<Toggle>();

		camSpline = Camera.main;//.GetComponent<CameraSpline>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        //Check the stored values of the settings
        volumeSlider.value = InvincibleScript.Instance.volume;
        dialoguesEnabled = InvincibleScript.Instance.dialogsEnabled;
        dialogueToggle.isOn = dialoguesEnabled;

        //Disable All canvas and buttons at start, then turn back on what we actually want to see.
        DisableAll();

        //Show the Splashscreen, the routine will flow over into mainmenu after given time.
        StartCoroutine("ShowSplash");
    }

    //dirty restart.
    void ReStart()
    {
        Debug.Log("Restart");
        dialogueCanvas = GameObject.Find("Chat").GetComponent<Canvas>();

        //Find references
        GetButtonReferences();

        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        dialogueToggle = GameObject.Find("DialoguesToggle").GetComponent<Toggle>();

        camSpline = Camera.main;//.GetComponent<CameraSpline>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        //Check the stored values of the settings
        volumeSlider.value = InvincibleScript.Instance.volume;
        dialoguesEnabled = InvincibleScript.Instance.dialogsEnabled;
        dialogueToggle.isOn = dialoguesEnabled;

        //Disable All canvas and buttons at start, then turn back on what we actually want to see.
        DisableAll();

        //Show the Splashscreen, the routine will flow over into mainmenu after given time.
        StartCoroutine("ShowSplash");
    }

    void Update()
    {
        if (AnyCanvasOn() || paused) {
            canvasOn = true;
           // camSpline.enabled = false;
        } else {
            canvasOn = false;
           // camSpline.enabled = true;
        }

        if (canvasOn || paused)
        {
            //very small so we can still use some functions based on time. --> use "* Time.timeScale"
            Time.timeScale = .0000001f;
        } else {
            Time.timeScale = 1;
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("START")) && !canvasOn) {
            ShowPauseMenu();
        }
    }

    #region "Button Clicks"
    public void PlayClick() //Part of MainMenu
    {
        DisableAll();

        StartCoroutine(StartGame());
   }

    public void ExitClick()  //Part of MainMenu
    {
        DisableAll();
        
        //We want the MainMenu to be just visible
        mainMenuCanvas.enabled = true;

        //Pop up the ExitMenu
        exitMenuCanvas.enabled = true;
        exitYesButton.enabled = true;
        exitNoButton.enabled = true;

        SelectButton(exitNo);
    }

    public void ExitYes()
    {

        Application.Quit();
    }  //Exits the application 

    public void ExitNo() //Part of ExitMenu
    {
        DisableAll();

        //Turn MainMenu back on
        mainMenuCanvas.enabled = true;
        optionsCanvas.enabled = true;
        playButton.enabled = true;
        exitButton.enabled = true;
        volumeSlider.enabled = true;
        dialogueToggle.enabled = true;

        SelectButton(play);
    }

    public void PauseContinue() //Part of PauseMenu
    {
        paused = false;
        //camSpline.enabled = true;

        //Kill all canvas and buttons because we want to continue playing.
        DisableAll();

        //Re-enable dialogues if they were playing.
        if (dialoguesPlaying) {
            dialogueCanvas.enabled = true;
        }
    }

    public void QuitClick() //Part of PauseMenu
    {
        DisableAll();

        //We want the PauseMenu to be just visible
        pauseMenuCanvas.enabled = true;

        //We clicked quit, so show QuitToMenu
        quitMenuCanvas.enabled = true;
        quitYesButton.enabled = true;
        quitNoButton.enabled = true;
        volumeSlider.enabled = true;
        dialogueToggle.enabled = true;

        SelectButton(quitNo);
    }

    public void QuitYes() //Part of QuitToMenu
    {
        paused = false;

        DisableAll();

        //We quit playing, show MainMenu again
        mainMenuCanvas.enabled = true;
        playButton.enabled = true;
        exitButton.enabled = true;

        SelectButton(play);

        InvincibleScript.Instance.volume = volumeSlider.value;
        InvincibleScript.Instance.showSplash = false;
        InvincibleScript.Instance.dialogsEnabled = dialoguesEnabled;
        InvincibleScript.firstLaunch = true;

        Application.LoadLevel(0);  //THIS SHOULD BE ON FOR BUILDS!
        Invoke("ReStart", 1 * Time.timeScale);
    }

    public void QuitNo() //Part of QuitToMenu
    {
        DisableAll();

        //We didnt want to quit after all, go back to PauseMenu
        pauseMenuCanvas.enabled = true;
        optionsCanvas.enabled = true;
        quitButton.enabled = true;
        continueButton.enabled = true;

        volumeSlider.enabled = true;
        dialogueToggle.enabled = true;

        SelectButton(cont);
    }
    #endregion "Button Clicks"

    bool AnyCanvasOn() {
        if (mainMenuCanvas.enabled || exitMenuCanvas.enabled || pauseMenuCanvas.enabled ||
            quitMenuCanvas.enabled || splashCanvas.enabled || optionsCanvas.enabled) {
            return true;
        }
        else return false;
    }

    public void ChangeVolume (Slider slider) {
		volume = slider.value;
		AudioListener.volume = volume;
	}

    public void DialogToggleChanged() {
        dialoguesEnabled = dialogueToggle.isOn;
    }

    void DisableAllButtons() {
        //MainMenu
        playButton.enabled = false;
        exitButton.enabled = false;

        //ExitMenu
        exitYesButton.enabled = false;
        exitNoButton.enabled = false;

        //PauseMenu
        quitButton.enabled = false;
        continueButton.enabled = false;

        //QuitToMenu
        quitYesButton.enabled = false;
        quitNoButton.enabled = false;

        volumeSlider.enabled = false;
        dialogueToggle.enabled = false;
    }

    void DisableAllCanvas() {
        mainMenuCanvas.enabled = false;     //MainMenu
        exitMenuCanvas.enabled = false;     //MainMenu_Exit
        pauseMenuCanvas.enabled = false;    //PauseMenu
        quitMenuCanvas.enabled = false;     //PauseMenu_Quit
        splashCanvas.enabled = false;       //Splashscreen
        optionsCanvas.enabled = false;      //OptionsCanvas
    }
    
    public void DisableAll() {
        DisableAllButtons();
        DisableAllCanvas();
    }

    void GetButtonReferences() {
        playButton = play.GetComponent<Button>();
        exitButton = exit.GetComponent<Button>();
        exitYesButton = exitYes.GetComponent<Button>();
        exitNoButton = exitNo.GetComponent<Button>();
        continueButton = cont.GetComponent<Button>();
        quitButton = quit.GetComponent<Button>();
        quitYesButton = quitYes.GetComponent<Button>();
        quitNoButton = quitNo.GetComponent<Button>();
    }

    void SelectButton(GameObject button) {
        eventSystem.SetSelectedGameObject(button);
    }

    
    /// <summary>
    /// Shows the MainMenu with some options.
    /// </summary>
    void ShowMainMenu() {
        mainMenuCanvas.enabled = true;
        optionsCanvas.enabled = true;

        playButton.enabled = true;
        exitButton.enabled = true;

        volumeSlider.enabled = true;
        dialogueToggle.enabled = true;

        SelectButton(play);
    }

    /// <summary>
    /// Shows the PauseMenu with some options.
    /// </summary>
    void ShowPauseMenu() {
        paused = true;

        pauseMenuCanvas.enabled = true;
        optionsCanvas.enabled = true;

        continueButton.enabled = true;
        quitButton.enabled = true;
        volumeSlider.enabled = true;
        dialogueToggle.enabled = true;

        if (dialogueCanvas.enabled) {
            dialoguesPlaying = true;
            dialogueCanvas.enabled = false;

            //if dialoguesplaying = true, be sure to enable it back when you press continue
        }

        SelectButton(cont);
    }

    // By starting the game using a coroutine with a small delay, we prevent instantly skipping the cutscene.
    IEnumerator StartGame() {
        if (InvincibleScript.firstLaunch) {
            yield return new WaitForSeconds(0.5f);
            camSpline = Camera.main;
            camSpline.GetComponent<CameraSwitch>().Play();
            InvincibleScript.firstLaunch = false;
        } else {
            Camera.main.GetComponent<CameraSwitch>().SwitchToNormal();
        }
            yield return null;
    }

    IEnumerator ShowSplash() {
        if (InvincibleScript.Instance.showSplash) { 
            splashCanvas.enabled = true;
            Time.timeScale = .0000001f;
            yield return new WaitForSeconds(splashTime * Time.timeScale);

            splashCanvas.enabled = false;
        }
        ShowMainMenu();
    }
}
