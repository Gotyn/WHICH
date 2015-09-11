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
    public bool clicker = false;
    public bool timeOut = false;

    public float clickTimer = 6.1f;

    CameraSpline cam;
    PlayerMovement sBro;
    PlayerMovement bBro;
    CheckIfGrounded checker;

    // Use this for initialization
    void Start()
    {
        menu = menu.GetComponent<Canvas>();
        exitMenu = exitMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        pauseMenu = pauseMenu.GetComponent<Canvas>();

        playButton = playButton.GetComponent<Button>();
        continueButton = continueButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();

        exitMenu.enabled = false;
        pauseMenu.enabled = false;
        quitMenu.enabled = false;

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSpline>();

        bBro = GameObject.FindGameObjectWithTag("Big").GetComponent<PlayerMovement>();
        sBro = GameObject.FindGameObjectWithTag("Small").GetComponent<PlayerMovement>();
        checker = sBro.GetComponent<CheckIfGrounded>();

    }

    void Update()
    {
        if(clicker)
        {
            clickTimer -= Time.deltaTime;
        }

        if (clickTimer <= 0)
        {
            timeOut = true;
        }
        else
            timeOut = false;

        if (menu.enabled || paused)
        {
            canvasOn = true;
            cam.enabled = false;
        }

        else
        {
            canvasOn = false;
            cam.enabled = true;
        }


        if (canvasOn || paused)
        {
            bBro.enabled = false;
            sBro.enabled = false;
			sBro.GetComponentInChildren<FireAttackScript>().enabled = false;
			bBro.GetComponentInChildren<HolderTest>().enabled = false;

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
        clicker = true;
        /*
		bBro.enabled = true;
		sBro.enabled = true;
		sBro.GetComponentInChildren<FireAttackScript>().enabled = true;
		bBro.GetComponentInChildren<HolderTest>().enabled = true;

    */
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
        bBro.enabled = true;
        sBro.enabled = true;
        cam.enabled = true;
		sBro.GetComponentInChildren<FireAttackScript>().enabled = true;
		bBro.GetComponentInChildren<HolderTest>().enabled = true;

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
        Application.LoadLevel(0);
    }

    public void quitNo()
    {
        quitMenu.enabled = false;
        continueButton.enabled = true;
        quitButton.enabled = true;
    }

}
