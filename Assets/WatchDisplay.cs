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
        if(GameManager.instance.firstDisk.locked == false)
        {
            activeDisk = GameManager.instance.firstDisk;
        }
        else
        {
            activeDisk = GameManager.instance.secondDisk;
        }
    }

    private void ObjectToDisplay()
    {
       if((activeDisk.installed && !activeDisk.repaired) || (!activeDisk.installed && activeDisk.repaired && activeDisk.welded))
        {
            //On matérialise la borne d'arcade sur la montre
        }
       else if (!activeDisk.installed && !activeDisk.welded && !activeDisk.repaired)
        {
            //On matérialise le marteau
        }
       else if ((!activeDisk.installed && activeDisk.welded && !activeDisk.repaired) || (!activeDisk.installed && !activeDisk.welded && activeDisk.repaired))
        {
            //On matérialise le chalumeau
        }
    }


    private void Update()
    {

    }
}
