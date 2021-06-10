using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] texts;
    public GameObject[] screens;

    public GameObject hudText;
    public GameObject crosshair;

    [Header("GetReady Screen")]
    public GameObject getReadyScreen;
    public Text countDown;
    public GameObject spawner;

    [Header("EndTutorialScreen")]
    public GameObject EndTutoialScreen;

    [Header("Instructions")]
    public GameObject instructionsScreen;
    public GameObject ins;
    public Image phone;
    public Image leftArrow;
    public Image rightArrow;
    public Text keepPhoneUp;

    public static TutorialManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        crosshair.SetActive(false);
        StartCoroutine(HUDscreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HUDscreen()
    {
        hudText.transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        hudText.SetActive(true);

        hudText.transform.LeanScale(Vector2.one, 1f);
        yield return new WaitForSeconds(3f);
        hudText.SetActive(false);

        screens[0].SetActive(true);

        yield break;
    }
    
    public void OnNext1()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(true);
        texts[1].GetComponent<TextAnimation>().Invoke("PlayAnimation",0.5f);
    }

    public void OnNext2()
    {
        screens[1].SetActive(false);
        screens[2].SetActive(true);
        texts[2].GetComponent<TextAnimation>().Invoke("PlayAnimation", 0.5f);
        texts[3].GetComponent<TextAnimation>().Invoke("PlayAnimation", 0.5f);
    }

    public void OnNext3()
    {
        screens[2].SetActive(false);
        screens[3].SetActive(true);
        texts[4].GetComponent<TextAnimation>().Invoke("PlayAnimation", 0.5f);
        texts[5].GetComponent<TextAnimation>().Invoke("PlayAnimation", 0.5f);
    }

    public void OnNext4()
    {
        screens[3].SetActive(false);
        screens[4].SetActive(true);
        texts[6].GetComponent<TextAnimation>().Invoke("PlayAnimation", 0.5f);
    }

    public void OnNext5()
    {
        screens[4].SetActive(false);
        getReadyScreen.SetActive(true);
        Instructions();
        StartCoroutine(GetReadyScreen());
    }

    IEnumerator GetReadyScreen()
    {
        countDown.transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(1);
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

        crosshair.SetActive(true);

        yield break;
    }

    void Instructions()
    {
        instructionsScreen.SetActive(true);

        phone.transform.LeanRotateX(0, 1f).setLoopPingPong();

        leftArrow.transform.LeanRotateZ(0, 1f).setLoopPingPong();
        leftArrow.transform.LeanRotateX(60, 1f).setLoopPingPong();
        leftArrow.transform.LeanMoveLocal(new Vector2(-130, -260), 1f).setLoopPingPong();

        rightArrow.transform.LeanRotateZ(0, 1f).setLoopPingPong();
        rightArrow.transform.LeanRotateX(60, 1f).setLoopPingPong();
        rightArrow.transform.LeanMoveLocal(new Vector2(130, -260), 1f).setLoopPingPong();

        keepPhoneUp.transform.localScale = Vector2.zero;
        keepPhoneUp.transform.LeanScale(Vector2.one, 2f);

    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("AmmunitionShop");
    }

 
    
}
