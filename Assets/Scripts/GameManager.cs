/*
    Oyunun genel iþleyiþinin ayarlandýðý script.
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
        wellDonePanel.SetActive(isComplated); // eðer bir görev tamamlandýysa tebrikler paneli açýlýyor.
        inGameMenu.SetActive(inGameMenuSet);

        if (inGameMenuSet) // esc tuþuna basýldýðýnda oyun içi menü açýlýyor ya da açýksa kapanýyor.
        {
            menuMusic.volume = 1;
        }
        else
            menuMusic.volume = 0;

        if (missionSet && !isSpawned)   // eðer aktif görev atamasý varsa ve herhangi bir görev prefab'ý spawnlanmamýþsa yeni görevi spawnla.
            appointMission(missionScript.instance.currentMission - 1);
        
        if(gameOn)
            Cursor.lockState = CursorLockMode.Locked;   // deðiþkene göre fare imlecini açýp kapatýyoruz.
        else
            Cursor.lockState = CursorLockMode.None;

        activeRoom = theRooms[(missionScript.instance.currentLevel - 1) % 4];           // görevde aktif olacak oda ve kontorl masasýný görev akýþýna göre ayarlýyoruz.
        activeTable = controlTables[(missionScript.instance.currentMission - 1) % 3];
    }
    public void appointMission(int mission) // görev spawný yapýyoruz
    {
        Instantiate(missionPrefabs[mission],new Vector3(0,0,0), Quaternion.identity);
        isSpawned = true;
    }
    
}
