using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    public ParticleSystem deathEffect;
    public float waitTimeBeforeEffect;

    public IEnumerator DeathEffect()
    {
        yield return new WaitForSeconds(waitTimeBeforeEffect);
        Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 0.0123f), Quaternion.identity);

        yield break;
    }
}
