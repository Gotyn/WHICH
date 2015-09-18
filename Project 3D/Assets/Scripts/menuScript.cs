using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class menuScript : MonoBehaviour {
    //Get a reference to the eventSystem so we can change the first selected button.
    EventSystem eventSystem;

    CameraSpline camSpline;
    PlayerMovement sBroMovement, bBroMovement;
    Slider volumeSliderMain, volumeSliderPause;

    public float splashTime = 3f;

    public Canvas mainMenuCanvas, 
                  exitMenuCanvas, 
                  pauseMenuCanvas, 
                  quitMenuCanvas,
                  splashCanvas;
    
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
    public float volume = 1;

    // Use this for initialization
    void Start() {
        //Find references
        GetButtonReferences();
        volumeSliderMain = GameObject.Find("VolumeSliderMain").GetComponent<Slider>();
        volumeSliderPause = GameObject.Find("VolumeSliderPause").GetComponent<Slider>();

        camSpline = Camera.main.GetComponent<CameraSpline>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        //Disable All canvas and buttons at start, then turn back on what we actually want to see.
        DisableAll();

        //Show the Splashscreen, the routine will flow over into mainmenu after given time.
        StartCoroutine("ShowSplash");
    }

    void Update()
    {
        if (mainMenuCanvas.enabled || exitMenuCanvas.enabled || pauseMenuCanvas.enabled || quitMenuCanvas.enabled || splashCanvas.enabled || paused) {
            canvasOn = true;
            camSpline.enabled = false;
        } else {
            canvasOn = false;
            camSpline.enabled = true;
        }

        if (canvasOn || paused)
        {
            //very small so we can still use some functions based on time. --> use "* Time.timeScale"
            Time.timeScale = .0000001f;
        } else {
            Time.timeScale = 1;
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("START")) && !canvasOn) {
            paused = true;
            continueButton.enabled = true;
            quitButton.enabled = true;
            volumeSliderPause.enabled = true;
            volumeSliderPause.value = volume;

            SelectButton(cont);
        }            

        if (paused && !pauseMenuCanvas.enabled)
            pauseMenuCanvas.enabled = true;
    }

    public void PlayClick() //Part of MainMenu
    {
        DisableAll();
		camSpline.GetComponent<CameraSwitch> ().Play ();
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
        playButton.enabled = true;
        exitButton.enabled = true;
        volumeSliderMain.enabled = true;
        volumeSliderMain.value = volume;

        SelectButton(play);
    }


    public void PauseContinue() //Part of PauseMenu
    {
        paused = false;
        camSpline.enabled = true;

        //Kill all canvas and buttons because we want to continue playing.
        DisableAll();

    }

    public void QuitClick() //Part of PauseMenu
    {
        DisableAll();
        
        //We clicked quit, so show QuitToMenu
        quitMenuCanvas.enabled = true;
        quitYesButton.enabled = true;
        quitNoButton.enabled = true;
        volumeSliderPause.enabled = true;
        volumeSliderPause.value = volume;

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

        Application.LoadLevel(0);  //THIS SHOULD BE ON FOR BUILDS!
    }

    public void QuitNo() //Part of QuitToMenu
    {
        DisableAll();

        //We didnt want to quit after all, go back to PauseMenu
        pauseMenuCanvas.enabled = true;
        quitButton.enabled = true;
        continueButton.enabled = true;

        volumeSliderPause.enabled = true;
        volumeSliderPause.value = volume;

        SelectButton(cont);
    }

	public void ChangeVolume (Slider slider) {
		volume = slider.value;
		AudioListener.volume = volume;
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

        //VolumeSliders  -- Set the correct value across all sliders before disabling them.
        volumeSliderMain.value = volume; 
        volumeSliderPause.value = volume;
        volumeSliderMain.enabled = false;
        volumeSliderPause.enabled = false;
    }

    void DisableAllCanvas() {
        mainMenuCanvas.enabled = false;
        exitMenuCanvas.enabled = false;
        pauseMenuCanvas.enabled = false;
        quitMenuCanvas.enabled = false;
        splashCanvas.enabled = false;
    }

    void DisableAll() {
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

    IEnumerator ShowSplash() {
        Vector4 transparent = new Vector4(255, 255, 255, 0f);
        Vector4 opaque = new Vector4(255, 255, 255, 1f);

        splashCanvas.enabled = true;
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(splashTime * Time.timeScale);

        splashCanvas.enabled = false;

        ShowMainMenu();
        SelectButton(play);
    }

    void ShowMainMenu() {
        mainMenuCanvas.enabled = true;
        playButton.enabled = true;
        exitButton.enabled = true;
        volumeSliderMain.enabled = true;
        volumeSliderMain.value = volume;
    }
}
