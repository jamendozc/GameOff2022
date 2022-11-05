using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    
    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
    public void GatherFood()
    {
        Debug.Log("Gather Food");
        OpenPanel();
    }
    public void GatherFlower()
    {
        Debug.Log("Gather Flowers");
        OpenPanel();
    }
    public void GatherRock()
    {
        Debug.Log("Gather Rocks");
        OpenPanel();
    }
    public void GatherWood()
    {
        Debug.Log("Gather Wood");
        OpenPanel();
    }
    public void Cook()
    {
        Debug.Log("Cooking");
        OpenPanel();
    }
    public void MakeMedicine()
    {
        Debug.Log("Making Medicine");
        OpenPanel();
    }
}
