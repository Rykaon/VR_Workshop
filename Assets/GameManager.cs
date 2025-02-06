using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DiskStatus
{
    public List<GrabbableAttachedObject> disk;

    public bool locked;//Verrouilée (Retirable de la borne d'arcade ou non)
    public bool welded;//Soudée (Si la disquette à sa plaque ou non)
    public bool repaired;//Réparée (Les puces de la cassettes sont toutes réparées)
    public bool installed;//Instalée (Si la disquette est sur la borne d'arcade ou non)
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    //Suppression d'une instance précédente (sécurité...)

        instance = this;
    }

    public enum GameState
    {
        Initialization,//Initialisation du jeu : Se termine lorsque les variables sont chargées
        FirstPhase,//Première phase de gameplay, se termine lorsque la première cassette est réparée et est mise dans la borne d'arcade
        SecondPhase,//Deuxieme phase de gameplay, se termine lorsque la deuxieme cassette est réparée et est mise dans la borne d'arcade
        EndGame//Fin du jeu
    }
    public GameState currentState;

    void Start()
    {
        //Début du jeu
        currentState = GameState.Initialization;
        DiskInitialization();
    }

    public DiskStatus firstDisk;
    public DiskStatus secondDisk;
    public DiskStatus[] allDiskInScene;

    public DiskStatusChecker checkDiskStatus;

    public void DiskInitialization()
    {
        currentState = GameState.FirstPhase;

        //firstDisk.disk = 
        firstDisk.locked = false;
        firstDisk.welded = true;
        firstDisk.repaired = false;
        firstDisk.installed = true;

        //secondDisk.disk =
        secondDisk.locked = true;
        secondDisk.welded = true;
        secondDisk.repaired = false;
        secondDisk.installed = true;

        //Pour toutes les cassettes qui sont en rab (afin d'éviter le soft lock)
        for (int i =0; i< allDiskInScene.Length; i++)
        {
            allDiskInScene[i].locked = false;
            allDiskInScene[i].welded = true;
            allDiskInScene[i].repaired = false;
            allDiskInScene[i].installed = false;
        }

        Debug.Log("Initialisé");
    }

    public void LaunchFirstPhase()
    {
        //SFX petits éclairs
        //FX petits éclairs

        //Musique monde réel
    }

    public void LaunchSecondPhase()
    {
        //SFX Level Up
        //FX Puissant éclair

        //Changer la gravité
        Physics.gravity = Vector3.zero;
    }

    void Update()
    {

    }
}
