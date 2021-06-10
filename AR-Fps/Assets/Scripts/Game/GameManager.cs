using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public int money;
    public bool isTutorial;

    [Header("Instructions")]
    public GameObject instructionsScreen;
    public GameObject ins;
    public Image phone;
    public Image leftArrow;
    public Image rightArrow;
    public Text countDown;
    public Text keepPhoneUp;

    [Header("Variables")]
    public GameObject spawner;
    public string weaponEquippedIs;

    //instance
    public static GameManager instance;

    private void Awake()
    {
        //set the instance to this script
        instance = this;
        LoadVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadMoney();//load money from player prefs
        //LoadVariables();//assign variables from PlayerPrefs

        if(!isTutorial)
            Instructions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        money += amount;
        GameUI.instance.UpdateMoney(); //update UI
        if(!isTutorial)
            PlayerPrefs.SetInt("Money", money); //saving money amount
    }

    void LoadMoney()
    {
        if (PlayerPrefs.HasKey("Money"))
            money = PlayerPrefs.GetInt("Money");
        else
            money = 0;

        GameUI.instance.UpdateMoney();
    }

    void LoadVariables()
    {
        Debug.Log(PlayerPrefs.GetString("EquippedWeapon"));
        if (PlayerPrefs.HasKey("EquippedWeapon"))
        {
            if (PlayerPrefs.GetString("EquippedWeapon") == "Gun")
                weaponEquippedIs = "Gun";
            else if (PlayerPrefs.GetString("EquippedWeapon") == "Meele")
                weaponEquippedIs = "Meele";
        }
        else
            weaponEquippedIs = "Gun";
    }

    void Instructions()
    {
        instructionsScreen.SetActive(true);

        phone.transform.LeanRotateX(0,1f).setLoopPingPong();

        leftArrow.transform.LeanRotateZ(0, 1f).setLoopPingPong();
        leftArrow.transform.LeanRotateX(60, 1f).setLoopPingPong();
        leftArrow.transform.LeanMoveLocal(new Vector2(-130, -260), 1f).setLoopPingPong();


        rightArrow.transform.LeanRotateZ(0, 1f).setLoopPingPong();
        rightArrow.transform.LeanRotateX(60, 1f).setLoopPingPong();
        rightArrow.transform.LeanMoveLocal(new Vector2(130, -260), 1f).setLoopPingPong();

        keepPhoneUp.transform.localScale = Vector2.zero;
        keepPhoneUp.transform.LeanScale(Vector2.one, 2f);

        StartCoroutine(GetReadyScreen());

    }
    IEnumerator GetReadyScreen()
    {
        countDown.transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        countDown.enabled = true;
        countDown.transform.LeanScale(Vector2.one, 1.5f);
        countDown.transform.localScale = Vector2.zero;

        yield return new WaitForSeconds(1.5f);
        countDown.text = "2";
        countDown.transform.LeanScale(Vector2.one, 1.5f);
        countDown.transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(1.5f);

        countDown.text = "1";
        countDown.transform.LeanScale(Vector2.one, 1.5f);
        countDown.transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(1.5f);

        countDown.enabled = false;
        yield return new WaitForSeconds(0.6f);

        instructionsScreen.SetActive(false);
        spawner.SetActive(true);


        yield break;
    }

    public void LooseGame()
    {
        GameUI.instance.looseGameScreen.SetActive(true);
    }

    public void OnShop()
    {
        SceneManager.LoadScene("AmmunitionShop");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExit()
    {
        Application.Quit();
    }
    
}
