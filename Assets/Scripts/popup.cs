using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    public GameObject Popup;
    public pauseMenu pauseScript;
    public Animator animator;

    public void display(int number)
    {
        Popup.SetActive(true);
        animator.SetInteger("glassAnim", number);
        pauseScript.isPaused = true;
    }

    public void hide()
    {
        animator.SetInteger("glassAnim", 0);
        Popup.SetActive(false);
        pauseScript.isPaused = false;
    }

}
