using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuceScript : MonoBehaviour
{
    public GrabbableAttachedObject disk;
    public int actualCounter = 0;
    public int stepCounter;
    private bool isRepaired = false;

    public List<Material> materials;

    public Renderer rendererPuce;
    int materialCount = 0;
    int maxCount = 9;

    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject obj) && actualCounter < stepCounter)
        {
            GrabbableTool tool = obj as GrabbableTool;

            stepCounter = 1;

            if (tool != null)
            {
                if (tool.isBlowTorch)
                {
                    stepCounter = 3;
                }
            }

            actualCounter += stepCounter;

            if (materialCount == 0 && actualCounter == 3)
            {
                materialCount++;
                rendererPuce.materials[0] = materials[materialCount];
            }
            else if (materialCount == 1 && actualCounter == 6)
            {
                materialCount++;
                rendererPuce.materials[0] = materials[materialCount];
            }
            else if (materialCount == 2 && actualCounter == 9)
            {
                materialCount++;
                rendererPuce.materials[0] = materials[materialCount];

                if (!isRepaired)
                {
                    Rafitstole();
                }
            }
        }

    }

    public void Rafitstole()
    {
        isRepaired = true;

        if (disk == GameManager.instance.firstDisk.disk)
        {
            GameManager.instance.checkDiskStatus.RepairDisk(GameManager.instance.firstDisk);
        }
        else if (disk = GameManager.instance.secondDisk.disk)
        {
            GameManager.instance.checkDiskStatus.RepairDisk(GameManager.instance.secondDisk);
        }
        else
        {
            for (int i = 0; i < GameManager.instance.allDiskInScene.Length; i++)
            {
                if (disk == GameManager.instance.allDiskInScene[i].disk)
                {
                    GameManager.instance.checkDiskStatus.RepairDisk(GameManager.instance.allDiskInScene[i]);
                }
            }
        }
        
    }

}


