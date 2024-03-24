/*
    Aktif seviye, g�rev ve tamamlanan g�revlerin takibasyonunun yap�ld��� script.
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

    public int[] levelsMissions = { 3, 3, 5, 9 }; // aktif levelde ka� g�rev olaca�� belirleniyor.

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetLevelsAndMissions();
    }
    public void MissionAccomplished() // g�rev tamamlan�nca �a��r�lan metot.
    {
        GameManager.instance.isActiveMission = false;
        GameManager.instance.missionSet = false;
        GameManager.instance.isComplated = true;
        audioSource.Play();

        complatedMissions++;

        if(currentMission == levelsMissions[currentLevel - 1]) // e�er tamamlanan g�rev say�s� seviyeye atanan g�rev say�s�na e�itse seviye bir art�yor.
        {
            complatedLevels++;
        }
        SetLevelsAndMissons();
    }
    void GetLevelsAndMissions() // kay�tl� g�rev ve level bilgileri al�n�p buna g�re aktif g�rev ve seviye tan�mlamalar� yap�l�yor.
    {
        complatedLevels = PlayerPrefs.GetInt("ComplatedLevels", 0);
        complatedMissions = PlayerPrefs.GetInt("ComplatedMissons", 0);
        currentLevel = complatedLevels + 1;
        currentMission = (complatedMissions % levelsMissions[currentLevel - 1]) + 1;
    }
    void SetLevelsAndMissons() // tamamlanan g�rev ve seviye bilgileri kay�tediliyor.
    {
        PlayerPrefs.SetInt("ComplatedLevels", complatedLevels);
        PlayerPrefs.SetInt("ComplatedMissons", complatedMissions);
        GetLevelsAndMissions();
    }
}
