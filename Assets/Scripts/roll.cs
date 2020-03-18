using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class roll : MonoBehaviour 
{
    // Create two sprites for the glasses : empty of full
    public Sprite glassFull;
    public Sprite glassEmpty;

    public Rigidbody rb; // Variable to the rigidbody to have physics interactions

    bool sleeping = true; // Used to check if the value have already been displayed and if the dice is still

    bool[] glassIsFull = { true, true, true, true, true, true }; // Array to know if each glass if full or not
    public GameObject[] glass = new GameObject[6]; // Array to store all the image gameobjects of the glasses

    public pauseMenu pauseScript;
    public popup popupScript;

    void Start()
    {
        Restart();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(rb.IsSleeping() && !sleeping) // If the dice is not moving && if we havn't displayed its value yet
        {
            sleeping = true; // The value will not be displayed as second time, till we roll the dice again
                             // We can throw it again because it has stopped moving

            //Debug.Log(transform.eulerAngles);

            for (int i = 0; i < 6; i++)
            {
                if (DiceIsEqual(i+1))
                    {
                    if (glassIsFull[i])
                    {
                        glassIsFull[i] = false;
                        glass[i].GetComponent<Image>().sprite = glassEmpty;
                    }
                    else
                    {
                        glassIsFull[i] = true;
                        glass[i].GetComponent<Image>().sprite = glassFull;
                        popupScript.display(i+1);
                    }
                    break;
                    }
            }
        
        }
    }

    void OnMouseDown() // When clicking
    {
        if (sleeping && !pauseScript.isPaused)
        {
            sleeping = false; // We are likely to throw the dice because he is not moving and display the value again 
            rb.AddForce(Random.Range(-5000, 5000), 7500, Random.Range(-5000, 5000)); // Add constant force along Y to make the dice go in the air, and random along X and Z
            rb.AddTorque(Random.Range(-50000, 50000), Random.Range(-50000, 50000), Random.Range(-5000, 5000)); // Add random torque to "roll" the dice
            // To be able to go in every directions we should begin at -xxx and not at 0
        }
    }

    bool DiceIsEqual(int value)
    {
        switch(value)
        {
            case 1:
                if ((transform.eulerAngles.x < 5 || transform.eulerAngles.x > 355) && (transform.eulerAngles.z < 185 && transform.eulerAngles.z > 175))
                    return true;
                else
                    return false;

            case 2:
                if ((transform.eulerAngles.x < 5 || transform.eulerAngles.x > 355) && (transform.eulerAngles.z < 275 && transform.eulerAngles.z > 265))
                    return true;
                else
                    return false;
                
            case 3:
                if ((transform.eulerAngles.x < 275 && transform.eulerAngles.x > 265) && (transform.eulerAngles.z < 5 || transform.eulerAngles.z > 355))
                    return true;
                else
                    return false;
                
            case 4:
                if ((transform.eulerAngles.x < 5 || transform.eulerAngles.x > 355) && (transform.eulerAngles.z < 95 && transform.eulerAngles.z > 85))
                    return true;
                else
                    return false;
                
            case 5:
                if ((transform.eulerAngles.x < 95 && transform.eulerAngles.x > 85) && (transform.eulerAngles.z < 5 || transform.eulerAngles.z > 355))
                    return true;
                else
                    return false;
                
            case 6:
                if ((transform.eulerAngles.x < 5 || transform.eulerAngles.x > 355) && (transform.eulerAngles.z < 5 || transform.eulerAngles.z > 355))
                    return true;
                else
                    return false;

            default:
                Debug.Log("Error");
                return false;

        }
    }

    public void Restart()
    {
        sleeping = true; //We don't want to display the message when moving the dice back to initial position

        for ( int i = 0; i < 6; i++)
        {
            glass[i].GetComponent<Image>().sprite = glassFull; // Change all the sprite to fill
            glassIsFull[i] = true; // Change all the boolean to true = filled
        }
       
        this.transform.position = new Vector3(0, (float)0.15, (float)-7.4); // Move the dice to its initial position


    }
}
