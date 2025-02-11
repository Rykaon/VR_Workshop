using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuceScript : MonoBehaviour
{
  
    private bool isRepaired = false;

    public List<Material> materials;

    public Renderer rendererPuce;
    public GameManager GM;
    public int materialCount = 0;
    int maxCount;

    private void Awake()
    {
        rendererPuce = GetComponent<Renderer>();
        maxCount = materials.Count-1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GM.firstDisk.repaired && !GM.firstDisk.welded)
        {
            materialCount++;
            rendererPuce.material = materials[materialCount];
            if (materialCount >= maxCount)
            {
                GM.firstDisk.repaired = true;
                
                
            }
        }
        
    }
    
    public void Rafitstole()
    {
        GM.firstDisk.repaired = true;

    }

    /*
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
        }*/
}


