using UnityEngine;

public class DiskStatusChecker : MonoBehaviour
{
    public bool IsDiskValid(DiskStatus diskToTest)
    {
        if(/*diskToTest.repaired == true && */diskToTest.welded == true  && diskToTest.installed == true && !GameManager.instance.secondPhase)
        {
            GameManager.instance.LaunchSecondPhase();
            return true;
        }
        else
        {
            //Ejecter la cassette de la borne
            //Son d'erreur
            return false;
        }
    }

    public bool DiskIsWelded(DiskStatus diskToTest)
    {
        return diskToTest.welded;
    }
    public void WeldDisk(DiskStatus diskToTest)
    {
        diskToTest.welded = true;
    }   
    public void UnweldDisk(DiskStatus diskToTest)
    {
        diskToTest.welded = false;
    }

    public bool DiskCanBeRepaired(DiskStatus diskToTest)
    {
        if (diskToTest.welded == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RepairDisk(DiskStatus diskToTest)
    {
        diskToTest.nbrPuceRepaired++;

        if (diskToTest.nbrPuceRepaired == diskToTest.nbrPuceToRepaired)
        {
            diskToTest.repaired = true;
        }
    }

    public void InstallDisk(DiskStatus diskToTest)
    {
        diskToTest.installed = true;
        IsDiskValid(diskToTest);
    }
    public void UnistallDisk(DiskStatus diskToTest)
    {
        diskToTest.installed = false;
    }
}
