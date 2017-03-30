using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //GameObject player;

    Camera m_Camera;

    public Text moneyText;
    static public int money;
    public int startingMoney;

    public Text scoreText;    
    static public int score;

    public GameObject gameUI;
    public GameObject pauseMenu;
    //public GameObject gameOverMenu;

    bool gameOver = false;

    GameObject enemySpawner;

    public GameObject[] turretPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject buttonPrefab;
    public GameObject[] buttonPositions;

    GameObject turretPrefab; 

    // if this is true, we're in "build mode" and the next click will place a building
    static public bool isBuilding = false;
    static public bool isRemoving = false;
    static public bool uiMode = false;

	// Use this for initialization
	void Start ()
    {
        pauseMenu.SetActive(false);
        //gameOverMenu.SetActive(false);
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner");
        enemySpawner.GetComponent<EnemySpawner>().enemyPrefab = enemyPrefabs;
        money = startingMoney;
        m_Camera = Camera.main;
        UpdateStats();
        ButtonIni();

    }

    void ButtonIni()
    {
        buttonPositions = GameObject.FindGameObjectsWithTag("ButtonPos").OrderBy(go => go.name).ToArray();
        int num = 0;
        if (buttonPositions.Length > 0)
        {
            foreach (GameObject prefab in turretPrefabs)
            {
                GameObject buildButton = Instantiate(buttonPrefab, buttonPositions[num].transform.position, Quaternion.identity, buttonPositions[num].transform);
                buildButton.GetComponent<BuildButton>().turretPrefab = prefab.GetComponent<TurretAI>().gameObject;
                num++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Construction();
        if (isRemoving || isBuilding || isPaused)
            uiMode = true;
        else
            uiMode = false;
    }

    public void BuildButton(GameObject turret)
    {
        if (turret != null)
        {
            isBuilding = true;
            isRemoving = !isBuilding;
            turretPrefab = turret;
        }
    }

    public void RemoveButton()
    {
        isRemoving = true;
        isBuilding = !isRemoving;
    }

    void Construction()
    {
        //This is for building turrets
        if (isBuilding && turretPrefab != null)
        {
            if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject() && money >= turretPrefab.GetComponent<TurretAI>().cost)
            {
                RaycastHit hit;
                if (Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider.tag == "BuildPos")
                    {
                        Instantiate(turretPrefab, hit.collider.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        money -= turretPrefab.GetComponent<TurretAI>().cost;
                        UpdateStats();
                    }
                    turretPrefab = null;
                    StartCoroutine(WaitTimer());
                }
            }
            else if (Input.GetButtonDown("Fire1") && money < turretPrefab.GetComponent<TurretAI>().cost)
                StartCoroutine(WaitTimer());
        }
        //This is for removing turrets
        if (isRemoving)
        {
            if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                if (Physics.Raycast(m_Camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider.tag == "Turret")
                    {
                        money += hit.collider.GetComponent<TurretAI>().cost / 2;
                        UpdateStats();
                        Destroy(hit.collider.gameObject);
                    }
                    StartCoroutine(WaitTimer());
                }
            }
        }
    }

    public void UpdateStats()
    {
        scoreText.text = "Score: " + score;
        moneyText.text = "Money: $" + money;
    }

    void AdminButton()
    {

    }

    bool isPaused = false;

    public void MenuPR(bool pause = false)
    {
        //Pause
        isPaused = pause;
        if (pause && !gameOver)
        {
            gameUI.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        if (!pause && !gameOver)
        {
            gameUI.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        if (!pause && gameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {

    }

    void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(0.2f);
        uiMode = false;
        if (isBuilding)
            isBuilding = !isBuilding;
        else if (isRemoving)
            isRemoving = !isRemoving;
    }
}