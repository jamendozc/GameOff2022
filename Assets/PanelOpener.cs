using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject GoBack;
    public GameObject critter;
    
    public void OpenPanel()
    {
        if(Panel != null && critter.GetComponent<CritterScript>().isBusy == false)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
        else
        {
            bool isActive = GoBack.activeSelf;
            GoBack.SetActive(!isActive);
        }
    }
    public void GatherFood()
    {
        Debug.Log("Gather Food");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = true;
    }
    public void Back()
    {
        Debug.Log("Go back");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = false;
    }
    public void GatherFlower()
    {
        Debug.Log("Gather Flowers");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = true;
    }
    public void GatherRock()
    {
        Debug.Log("Gather Rocks");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = true;
    }
    public void GatherWood()
    {
        Debug.Log("Gather Wood");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = true;
    }
    public void Cook()
    {
        Debug.Log("Cooking");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = true;
    }
    public void MakeMedicine()
    {
        Debug.Log("Making Medicine");
        OpenPanel();
        critter.GetComponent<CritterScript>().isBusy = true;
    }
}
