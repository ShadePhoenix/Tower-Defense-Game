using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    //GameObject player;

    Camera m_Camera;

    public Text moneyText;
    static public int money;
    public int startingMoney;

    public Text scoreText;    
    static public int score;

    GameObject enemySpawner;

    public GameObject[] turretPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject buttonPrefab;
    public GameObject[] buttonPositions;

    GameObject turretPrefab; 

    // if this is true, we're in "build mode" and the next click will place a building
    static public bool isBuilding = false;
    static public bool isRemoving = false;
    static public bool constructionMode = false;

	// Use this for initialization
	void Start ()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner");
        enemySpawner.GetComponent<EnemySpawner>().enemyPrefab = enemyPrefabs;
        money = startingMoney;
        m_Camera = Camera.main;
        //player = GameObject.FindGameObjectWithTag("Player");
        UpdateMoney();
        UpdateScore();
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
        if (isRemoving || isBuilding)
        {
            constructionMode = true;
        }
        else
            constructionMode = false;
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
                        UpdateMoney();
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
                        UpdateMoney();
                        Destroy(hit.collider.gameObject);
                    }
                    StartCoroutine(WaitTimer());
                }
            }
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateMoney()
    {
        moneyText.text = "Money: $" + money;
    }

    void AdminButton()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(0.2f);
        constructionMode = false;
        if (isBuilding)
            isBuilding = !isBuilding;
        else if (isRemoving)
            isRemoving = !isRemoving;
    }
}