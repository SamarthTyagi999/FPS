using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGun : MonoBehaviour
{
    public int maxAmmo = 50;
    public int clipSize = 10;
    [HideInInspector]
    public int currentAmmo;

    public float reloadSpeed = 2f;

    [HideInInspector]
    public bool needReload;

    public Text ammoText;
    public Animator anim;
    private AudioSource audioSource;
    public AudioClip reloadSFX;

    bool playedOnce;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = clipSize;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmo <= 0)
        {
            needReload = true;
            StartCoroutine(Reload());
        }
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }

    IEnumerator Reload()
    {
        
        anim.SetBool("Reload",true);

        if(!playedOnce)
            audioSource.PlayOneShot(reloadSFX);
        playedOnce = true;

        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = clipSize;
        needReload = false;
        anim.SetBool("Reload", false);
        playedOnce = false;
    }

    public void OnReload()
    {
        StartCoroutine(Reload());
        
    }
    void PlaySound()
    {
        audioSource.PlayOneShot(reloadSFX);
    }
}
