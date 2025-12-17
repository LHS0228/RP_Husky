using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public SaveData currentSaveData;

    private string saveFilePath;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // 저장 경로 설정 (PC, 모바일 다 됨)
        saveFilePath = Path.Combine(Application.persistentDataPath, "MySaveData.json");
    }

    // [저장하기]
    public void SaveGame()
    {
        // 1. 데이터를 JSON 텍스트로 변환 (예쁘게 보라고 true 옵션 줌)
        string jsonText = JsonUtility.ToJson(currentSaveData, true);

        // 2. 파일로 쓰기
        File.WriteAllText(saveFilePath, jsonText);

        Debug.Log($"[저장 완료] 경로: {saveFilePath}");
    }

    // [불러오기]
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // 1. 파일 읽기
            string jsonText = File.ReadAllText(saveFilePath);

            // 2. 텍스트를 데이터로 변환
            currentSaveData = JsonUtility.FromJson<SaveData>(jsonText);

            Debug.Log("[불러오기 성공]");
        }
        else
        {
            // 파일이 없으면? (첫 실행) -> 새 데이터 생성
            Debug.Log("[새 게임] 저장된 파일이 없습니다.");
            currentSaveData = new SaveData();

            // 기본 강아지 한 마리 줘야겠죠?
            currentSaveData.dogList.Add(new DogData("파트너", 50, 50, 50));
        }
    }
}
