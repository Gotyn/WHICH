using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {
    public Canvas menu;
    public Canvas exitMenu;
    public Canvas pauseMenu;
    public Canvas quitMenu;

    public Button playButton;
    public Button continueButton;
    public Button exitButton;
    public Button quitButton;

    public bool canvasOn;
    public bool paused;
	

    CameraSpline camSpline;
    PlayerMovement sBroMovement;
    PlayerMovement bBroMovement;

	public float volume =1;
	Slider volumeslider; 

    // Use this for initialization
    void Start()
    {
		volumeslider = GameObject.Find("Slider").GetComponent<Slider>();

        exitMenu.enabled = false;
        pauseMenu.enabled = false;
        quitMenu.enabled = false;

        camSpline = Camera.main.GetComponent<CameraSpline>();

        bBroMovement = GameObject.FindGameObjectWithTag("Big").GetComponent<PlayerMovement>();
        sBroMovement = GameObject.FindGameObjectWithTag("Small").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (menu.enabled || paused)
        {
            canvasOn = true;
            camSpline.enabled = false;
        }

        else
        {
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

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("START") && !canvasOn)
            paused = true;


        if (paused)
            pauseMenu.enabled = true;
        else
            pauseMenu.enabled = false;


    }

    public void playClick()
    {
        menu.enabled = false;
        
		bBroMovement.enabled = true;
		sBroMovement.enabled = true;
		sBroMovement.GetComponentInChildren<FireAttackScript>().enabled = true;
		bBroMovement.GetComponentInChildren<HolderTest>().enabled = true;

		camSpline.GetComponent<CameraSwitch> ().Play ();
   }

    public void exitClick()
    {
        exitMenu.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        quitButton.enabled = false;
        continueButton.enabled = false;

    }

    public void exitYes()
    {
        Application.Quit();

    }

    public void exitNo()
    {
        exitMenu.enabled = false;
        playButton.enabled = true;
        exitButton.enabled = true;
        quitButton.enabled = false;
        continueButton.enabled = false;
    }

    public void pauseQuit()
    {
        quitMenu.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        quitButton.enabled = true;
        continueButton.enabled = true;
    }

    public void cont()
    {
        paused = false;
        pauseMenu.enabled = false;
        bBroMovement.enabled = true;
        sBroMovement.enabled = true;
        camSpline.enabled = true;
		sBroMovement.GetComponentInChildren<FireAttackScript>().enabled = true;
		bBroMovement.GetComponentInChildren<HolderTest>().enabled = true;

    }

    public void quitClick()
    {
        quitMenu.enabled = true;
        continueButton.enabled = false;
        quitButton.enabled = false;
    }

    public void quitYes()
    {
        menu.enabled = true;
        quitMenu.enabled = false;
        paused = false;
       // Application.LoadLevel(0);
    }

    public void quitNo()
    {
        quitMenu.enabled = false;
        continueButton.enabled = true;
        quitButton.enabled = true;
    }

	public void ChangeVolume () {
		volume = volumeslider.value;
		AudioListener.volume = volume;
	}

}
