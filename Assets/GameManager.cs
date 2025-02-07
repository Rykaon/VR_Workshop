using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public struct DiskStatus
{
    public GrabbableAttachedObject disk;
    public bool locked;//Verrouilée (Retirable de la borne d'arcade ou non)
    public bool welded;//Soudée (Si la disquette à sa plaque ou non)
    public bool repaired;//Réparée (Les puces de la cassettes sont toutes réparées)
    public int nbrPuceRepaired;
    public int nbrPuceToRepaired;
    public bool installed;//Instalée (Si la disquette est sur la borne d'arcade ou non)
    
}

public class GameManager : MonoBehaviour
{
    public static event Action StartNoGrav;
    public static GameManager instance { get; private set; }
    public bool secondPhase = false;
    public PlayerMovement playerMovement;
    public List<GrabbableAttachedObject> disks;
    public Animator murs;
    public Animator sol;
    public InputActionProperty tr;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    //Suppression d'une instance précédente (sécurité...)

        instance = this;

        tr.action.started += ctx => LaunchSecondPhase();
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
        DiskInitialization(disks[0], disks[1]);
    }

    public DiskStatus firstDisk;
    public DiskStatus secondDisk;
    public DiskStatus[] allDiskInScene;

    public DiskStatusChecker checkDiskStatus;

    public void DiskInitialization(GrabbableAttachedObject firstDiskObject, GrabbableAttachedObject secondDiskObject)
    {
        currentState = GameState.FirstPhase;

        firstDisk.disk = firstDiskObject;
        firstDisk.locked = true;
        firstDisk.welded = true;
        firstDisk.repaired = false;
        firstDisk.nbrPuceRepaired = 0;
        firstDisk.nbrPuceToRepaired = 4;
        firstDisk.installed = true;

        secondDisk.disk = secondDiskObject;
        secondDisk.locked = true;
        secondDisk.welded = true;
        secondDisk.repaired = false;
        secondDisk.nbrPuceRepaired = 0;
        secondDisk.nbrPuceToRepaired = 4;
        secondDisk.installed = true;

        //Pour toutes les cassettes qui sont en rab (afin d'éviter le soft lock)
        /*for (int i =0; i< allDiskInScene.Length; i++)
        {
            allDiskInScene[i].locked = false;
            allDiskInScene[i].welded = true;
            allDiskInScene[i].repaired = false;
            allDiskInScene[i].nbrPuceRepaired = 0;
            allDiskInScene[i].nbrPuceToRepaired = 4;
            allDiskInScene[i].installed = false;
        }*/

        Debug.Log("Initialisé");
    }

    public void LaunchFirstPhase()
    {
      
    }

    public void LaunchSecondPhase()
    {
        if (!secondPhase)
        {
            
            secondPhase = true;
            Debug.Log(secondPhase);
            playerMovement.gravity = false;
            StartNoGrav?.Invoke() ;
            murs.SetBool("Debut", true);
            sol.SetBool("Debut", true);
            //SFX Level Up
            //FX Puissant éclair

            //Changer la gravité
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchSecondPhase();
        }
    }
}
