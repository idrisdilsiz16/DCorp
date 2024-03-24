/*
    Oyunun genel i�leyi�inin ayarland��� script.
 */
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string[] theRooms = { "Room1", "Room2", "Room3", "Room4" };
    public string[] controlTables = { "ControlTableLeft", "ControlTableMiddle", "ControlTableRight" };
    
    public string activeRoom,
                  activeTable,
                  playerName;
    public AudioSource menuMusic;

    public GameObject inGameMenu,
                      wellDonePanel;
    public GameObject[] missionPrefabs;

    public bool gameOn,
                missionSet,
                inGameMenuSet,
                isActiveMission,
                isSpawned,
                isComplated,
                isTutorialOn;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameOn = true;
        missionSet = false;
        inGameMenuSet = false;
        isActiveMission = false;
        isSpawned = false;
        isComplated = false;
        isTutorialOn = true;
        playerName = PlayerPrefs.GetString("playerName");
    }

    void Update()
    {
        wellDonePanel.SetActive(isComplated); // e�er bir g�rev tamamland�ysa tebrikler paneli a��l�yor.
        inGameMenu.SetActive(inGameMenuSet);

        if (inGameMenuSet) // esc tu�una bas�ld���nda oyun i�i men� a��l�yor ya da a��ksa kapan�yor.
        {
            menuMusic.volume = 1;
        }
        else
            menuMusic.volume = 0;

        if (missionSet && !isSpawned)   // e�er aktif g�rev atamas� varsa ve herhangi bir g�rev prefab'� spawnlanmam��sa yeni g�revi spawnla.
            appointMission(missionScript.instance.currentMission - 1);
        
        if(gameOn)
            Cursor.lockState = CursorLockMode.Locked;   // de�i�kene g�re fare imlecini a��p kapat�yoruz.
        else
            Cursor.lockState = CursorLockMode.None;

        activeRoom = theRooms[(missionScript.instance.currentLevel - 1) % 4];           // g�revde aktif olacak oda ve kontorl masas�n� g�rev ak���na g�re ayarl�yoruz.
        activeTable = controlTables[(missionScript.instance.currentMission - 1) % 3];
    }
    public void appointMission(int mission) // g�rev spawn� yap�yoruz
    {
        Instantiate(missionPrefabs[mission],new Vector3(0,0,0), Quaternion.identity);
        isSpawned = true;
    }
    
}
