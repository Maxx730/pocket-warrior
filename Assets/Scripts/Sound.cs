using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource CollectCoin;

    private void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        CollectCoin = sources[0];
    }
}
