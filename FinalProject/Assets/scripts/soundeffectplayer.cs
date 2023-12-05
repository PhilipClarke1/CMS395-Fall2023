using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundeffectplayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1, sfx2, sfx3;

    public void swordNoHit()
    {
        //Debug.Log("swordfnosdfsdf");
        src.clip = sfx1;
        src.Play();
    }

    public void punchSound() {
        src.clip = sfx2;
        src.Play();
    }

    public void swordHit()
    {
        src.clip = sfx3;
        src.Play();
    }

}
