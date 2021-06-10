using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInstructions : MonoBehaviour
{
    public GameObject pointer,pointer2;
    public GameObject instruction1Text, instruction2Text , instruction3Text, instruction4Text;
    public GameObject selectionScreen;

    // Start is called before the first frame update
    void Start()
    {
        instruction1Text.transform.localScale = instruction2Text.transform.localScale = instruction3Text.transform.localScale = instruction4Text.transform.localScale= Vector2.zero;
        instruction1Text.SetActive(false);
        instruction2Text.SetActive(false);
        instruction3Text.SetActive(false);
        instruction4Text.SetActive(false);
        pointer.SetActive(false);
        pointer2.SetActive(false);

        selectionScreen.SetActive(false);
        StartCoroutine( Instructions());   
    }

    IEnumerator Instructions()
    {
        instruction1Text.SetActive(true);
        instruction1Text.transform.LeanScale(Vector2.one, 1f);
        yield return new WaitForSeconds(2.5f);
        instruction1Text.SetActive(false);

        if (GetComponent<ARTapToPlace>().isShopPlaced == false)
        {
            instruction2Text.SetActive(true);
            instruction2Text.transform.LeanScale(Vector2.one, 1f);
            pointer.SetActive(true);
            pointer.transform.LeanRotateX(30f, 0.5f).setLoopPingPong(); 
        }
        yield break;
    }

    public IEnumerator Instructions2()
    {
        instruction3Text.SetActive(true);
        instruction3Text.transform.LeanScale(Vector2.one, 1f);
        yield return new WaitForSeconds(3f);
        instruction3Text.SetActive(false);

        instruction4Text.SetActive(true);
        instruction4Text.transform.LeanScale(Vector2.one, 1f);
        pointer2.SetActive(true);
        yield return new WaitForSeconds(3f);
        instruction4Text.SetActive(false);
        pointer2.SetActive(false);

        yield break;
    }
}
