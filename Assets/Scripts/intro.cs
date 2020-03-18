using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro : MonoBehaviour
{

    public GameObject anim; // intro canvas

    void Start()
    {
        anim.gameObject.SetActive(true); // Set the intro canvas to true -> play its animation
    }

}
