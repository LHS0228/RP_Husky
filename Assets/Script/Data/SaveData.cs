using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

// 호감도 데이터 한 덩어리
[System.Serializable]
public class RelationshipData
{
    public string npcName; // NPC 이름 (ID 역할)
    public int score;      // 호감도 점수

    public RelationshipData(string name, int value)
    {
        npcName = name;
        score = value;
    }
}

[System.Serializable]
public class SaveData
{
    // 여기에 저장하고 싶은 모든 걸 다 때려 넣으세요.
    public int playerGold; //소지금

    // 가지고 있는 강아지 리스트
    public List<DogData> dogList = new List<DogData>();

    // 모든 NPC의 호감도 리스트
    public List<RelationshipData> relationshipList = new List<RelationshipData>();
}
