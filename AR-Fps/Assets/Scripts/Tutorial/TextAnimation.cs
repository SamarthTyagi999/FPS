using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public bool isFirstText;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.zero;
        if (isFirstText)
            PlayAnimation();
    }

    public void PlayAnimation()
    {
        transform.LeanScale(Vector2.one, 1f);
    }
}
