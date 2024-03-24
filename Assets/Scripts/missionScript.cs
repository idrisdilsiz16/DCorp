/*
    Aktif seviye, görev ve tamamlanan görevlerin takibasyonunun yapýldýðý script.
 */
using UnityEngine;

public class missionScript : MonoBehaviour
{
    public static missionScript instance;
    private AudioSource audioSource;

    public int currentLevel,
               currentMission,
               complatedLevels,
               complatedMissions;

    public int[] levelsMissions = { 3, 3, 5, 9 }; // aktif levelde kaç görev olacaðý belirleniyor.

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetLevelsAndMissions();
    }
    public void MissionAccomplished() // görev tamamlanýnca çaðýrýlan metot.
    {
        GameManager.instance.isActiveMission = false;
        GameManager.instance.missionSet = false;
        GameManager.instance.isComplated = true;
        audioSource.Play();

        complatedMissions++;

        if(currentMission == levelsMissions[currentLevel - 1]) // eðer tamamlanan görev sayýsý seviyeye atanan görev sayýsýna eþitse seviye bir artýyor.
        {
            complatedLevels++;
        }
        SetLevelsAndMissons();
    }
    void GetLevelsAndMissions() // kayýtlý görev ve level bilgileri alýnýp buna göre aktif görev ve seviye tanýmlamalarý yapýlýyor.
    {
        complatedLevels = PlayerPrefs.GetInt("ComplatedLevels", 0);
        complatedMissions = PlayerPrefs.GetInt("ComplatedMissons", 0);
        currentLevel = complatedLevels + 1;
        currentMission = (complatedMissions % levelsMissions[currentLevel - 1]) + 1;
    }
    void SetLevelsAndMissons() // tamamlanan görev ve seviye bilgileri kayýtediliyor.
    {
        PlayerPrefs.SetInt("ComplatedLevels", complatedLevels);
        PlayerPrefs.SetInt("ComplatedMissons", complatedMissions);
        GetLevelsAndMissions();
    }
}
