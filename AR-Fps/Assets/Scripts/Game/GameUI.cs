using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public Text moneyText;
    //public Text ammoText;
    public Image healthBarFill;
    public Image gunImage;
    public Slider waveIndicatorSlider;
    public Image damageEffect;
    public GameObject gunFireButton;
    public GameObject meeleAttackButton;
    public GameObject reloadButton;

    public static GameUI instance;

    [Header("Display")]
    public Text waveNo;
    public GameObject waveNoBackground;
    public int totalEnemies;
    public int enemiesKilled;

    [Header("Screens")]
    public GameObject looseGameScreen;

    [Header("Sounds")]
    public AudioSource audioSource;
    float lastHeadShotSound=0f;


    private void Awake()
    {
        //set instance to this script
        instance = this;    
    }

    public void UpdateHealthbar(int curHp, int maxHp)
    {
        healthBarFill.fillAmount = (float)curHp / (float)maxHp;
    }

    public void UpdateMoney()
    {
        moneyText.text = "$"+GameManager.instance.money.ToString();
    }

    public void AttackButton()
    {
        if (GameManager.instance.weaponEquippedIs == "Meele")
        {
            meeleAttackButton.SetActive(true);
            gunFireButton.SetActive(false);
            reloadButton.SetActive(false);
        }
        else
        {
            meeleAttackButton.SetActive(false);
            gunFireButton.SetActive(true);
            reloadButton.SetActive(true);
        }

    }

    public void DisplayWaveNumber(int no)
    {
        StartCoroutine(UpdateWaveNumber(no));
    }

    IEnumerator UpdateWaveNumber(int currentwave)
    {
        waveNo.enabled = true;
        waveNoBackground.SetActive(true);
        waveNo.text ="WAVE " +(currentwave + 1).ToString()+" Incoming!";

        yield return new WaitForSeconds(2);

        waveNo.enabled = false;
        waveNoBackground.SetActive(false);
        yield break;
    }

    public void SetWaveSlider(int max)
    {
        waveIndicatorSlider.maxValue = max;
        waveIndicatorSlider.minValue = 0;
        waveIndicatorSlider.value = 0;
    }

    public void UpdateWaveSlider(int Killed)
    {
        waveIndicatorSlider.value += Killed;
    }

    public void UpdateDamageEffect(int value)
    {
        damageEffect.enabled = true;

        var tempColor = damageEffect.color;
        float alpha=value;
        
        alpha = (float)(1 - (alpha / 100));

        Debug.Log("Alpha = " + alpha);
        tempColor.a = alpha;

        damageEffect.color = tempColor;

        Invoke("TurnOffDamageEffect", 1.75f);
    }

    void TurnOffDamageEffect()
    {
        damageEffect.enabled = false;
    }

    public void HeadShot()
    {
        //Play HeadShot Sound
        Debug.Log("HeadShot");
        if (Time.time - lastHeadShotSound >= 4f)
        {
            audioSource.Play();
            lastHeadShotSound = Time.time;
        }
    }
}
