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

    public Canvas mainMenuCanvas, 
                  exitMenuCanvas, 
                  pauseMenuCanvas, 
                  quitMenuCanvas;
    
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
        bBroMovement = GameObject.FindGameObjectWithTag("Big").GetComponent<PlayerMovement>();
        sBroMovement = GameObject.FindGameObjectWithTag("Small").GetComponent<PlayerMovement>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        //Disable All canvas and buttons at start, then turn back on what we actually want to see.
        DisableAll();
        mainMenuCanvas.enabled = true;
        playButton.enabled = true;
        exitButton.enabled = true;
        volumeSliderMain.enabled = true;
        volumeSliderMain.value = volume;

        SelectButton(play);
    }

    void Update()
    {
        if (mainMenuCanvas.enabled || exitMenuCanvas.enabled || pauseMenuCanvas.enabled || quitMenuCanvas.enabled || paused) {
            canvasOn = true;
            camSpline.enabled = false;
        } else {
            canvasOn = false;
            camSpline.enabled = true;
        }

        if (canvasOn || paused)
        {
            bBroMovement.enabled = false;
            sBroMovement.enabled = false;
			sBroMovement.GetComponentInChildren<FireAttackScript>().enabled = false;
			bBroMovement.GetComponentInChildren<HolderTest>().enabled = false;
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

		bBroMovement.enabled = true;
		sBroMovement.enabled = true;
		sBroMovement.GetComponentInChildren<FireAttackScript>().enabled = true;
		bBroMovement.GetComponentInChildren<HolderTest>().enabled = true;

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

        bBroMovement.enabled = true;
        sBroMovement.enabled = true;
        camSpline.enabled = true;
		sBroMovement.GetComponentInChildren<FireAttackScript>().enabled = true;
		bBroMovement.GetComponentInChildren<HolderTest>().enabled = true;

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

        //Application.LoadLevel(0);  //THIS SHOULD BE ON FOR BUILDS!
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
        Debug.Log("VOLUME: " + volume);
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
}
