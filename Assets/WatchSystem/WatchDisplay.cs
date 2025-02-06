using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchDisplay : MonoBehaviour
{
    public bool WatchIsOn;

    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject welder;
    [SerializeField] private GameObject arcade;

    private DiskStatus activeDisk;

    public void Start()
    {
        SelectActiveDisk();
    }

    private void SelectActiveDisk()
    {
        if(!GameManager.instance.firstDisk.locked)
        {
            activeDisk = GameManager.instance.firstDisk;
        }
        else
        {
            activeDisk = GameManager.instance.secondDisk;
        }
    }

    private void DisplayObject()
    {
       if((activeDisk.installed && !activeDisk.repaired) || (!activeDisk.installed && activeDisk.repaired && activeDisk.welded))
        {
            //On matérialise la borne d'arcade sur la montre
            hammer.SetActive(false);
            welder.SetActive(false);
            arcade.SetActive(true);
        }
       else if (!activeDisk.installed && !activeDisk.welded && !activeDisk.repaired)
        {
            //On matérialise le marteau
            welder.SetActive(false);
            arcade.SetActive(false);
            hammer.SetActive(true);
        }
       else if ((!activeDisk.installed && activeDisk.welded && !activeDisk.repaired) || (!activeDisk.installed && !activeDisk.welded && activeDisk.repaired))
        {
            //On matérialise le chalumeau
            hammer.SetActive(false);
            arcade.SetActive(false);
            welder.SetActive(true);
        }
       else if(activeDisk.installed && activeDisk.welded && activeDisk.repaired)
        {
            activeDisk.locked = true;
            SelectActiveDisk();
        }
       else
        {
            //On a pas d'objectifs.
            hammer.SetActive(false);
            arcade.SetActive(false);
            welder.SetActive(false);
        }
    }

    private void DisplayOff()
    {
        hammer.SetActive(false);
        arcade.SetActive(false);
        welder.SetActive(false);
    }

    private void Update()
    {
        if(WatchIsOn)
        {
            DisplayObject();
        }
        else
        {
            DisplayOff();
        }
    }
}
