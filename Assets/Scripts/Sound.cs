using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource CollectCoin;
    public AudioSource BowRelease;

    private void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        CollectCoin = sources[0];
        BowRelease = sources[1];
    }
}
