using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder_play : MonoBehaviour
{
    public AudioSource thunder;
    public float delay;
    void Start()
    {
        thunder.PlayDelayed(delay);
    }
}
