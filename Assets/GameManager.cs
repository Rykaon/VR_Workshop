using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DiskStatus
{
    public List<GrabbableAttachedObject> disk;

    public bool locked;//Verrouil�e (Retirable de la borne d'arcade ou non)
    public bool welded;//Soud�e (Si la disquette � sa plaque ou non)
    public bool repaired;//R�par�e (Les puces de la cassettes sont toutes r�par�es)
    public bool installed;//Instal�e (Si la disquette est sur la borne d'arcade ou non)
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    //Suppression d'une instance pr�c�dente (s�curit�...)

        instance = this;
    }

    public enum GameState
    {
        Initialization,//Initialisation du jeu : Se termine lorsque les variables sont charg�es
        FirstPhase,//Premi�re phase de gameplay, se termine lorsque la premi�re cassette est r�par�e et est mise dans la borne d'arcade
        SecondPhase,//Deuxieme phase de gameplay, se termine lorsque la deuxieme cassette est r�par�e et est mise dans la borne d'arcade
        EndGame//Fin du jeu
    }
    GameState currentState;

    void Start()
    {
        //D�but du jeu
        currentState = GameState.Initialization;
        Initialization();
    }

    public DiskStatus firstDisk;
    public DiskStatus secondDisk;

    public DiskStatusChecker checkDiskStatus;

    public void Initialization()
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
    }

    public void LaunchSecondPhase()
    {
        //Changer la gravit� et tout le bordel qui va avec
    }

    void Update()
    {

    }
}
