using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public bool isFirstTime;
    public GameObject pointer;

    [Header("Screens")]
    public GameObject backgroundBlur;
    public GameObject startScreen, firstTimeScreen;

    [Header("Texts")]
    public GameObject text1;
    public GameObject text2;
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
            StartCoroutine(PlayingForTheFirstTime());
        PlayerPrefs.SetInt("FirstTime", 1);

    }


    void Update()
    {
        
    }

    IEnumerator PlayingForTheFirstTime()
    {
        startScreen.SetActive(false);
        firstTimeScreen.SetActive(true);
        backgroundBlur.SetActive(false);
        pointer.SetActive(false);

        text1.transform.localScale = Vector2.zero;
        text2.transform.localScale = Vector2.zero;

        yield return new WaitForSeconds(2f);

        //animating text1
        text1.SetActive(true);
        text1.transform.LeanScale(Vector2.one, 1f);
        yield return new WaitForSeconds(3f);
        text1.SetActive(false);

        backgroundBlur.SetActive(true);
        pointer.SetActive(true);

        //animating text2
        text2.SetActive(true);
        text2.transform.LeanScale(Vector2.one, 1f);
        yield return new WaitForSeconds(2f);


        yield break;
    }
    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnShop()
    {
        SceneManager.LoadScene("AmmunitionShop");
    }

    public void OnTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnExit()
    {
        PlayerPrefs.DeleteAll();
    }
}
