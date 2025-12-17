using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DogData
{
    // ==========================================
    // 1. 기본 신상 (Identity)
    // ==========================================
    public string dogID;        // 고유 번호 (예: "DOG_20231025_001")
    public string dogName;      // 이름 (유저가 지어줌)
    public int age;             // 나이 (년)
    public bool isLeader;       // 현재 리더인가? (포지션)

    // ==========================================
    // 2. 외형 데이터 (Visual Genotype)
    // * 이미지가 아니라 '번호(Index)'나 '색상값'만 저장합니다.
    // ==========================================
    public int bodyColorIndex;  // 몸통 색 팔레트 번호 (0:회색, 1:검정...)
    public int eyeColorIndex;   // 눈 색 팔레트 번호
    public bool hasPattern;     // 무늬 유무

    // ==========================================
    // 3. 능력치 (Stats)
    // * 0 ~ 100 기준. 높을수록 좋음.
    // ==========================================
    public int statSpeed;       // 속도 (이동 속도)
    public int statPower;       // 힘 (무게 저항)
    public int statStamina;     // 지구력 (달리는 시간)

    // ==========================================
    // 4. 상태 (Current Status)
    // * 게임 도중 계속 변하는 값
    // ==========================================
    public float currentEnergy; // 현재 에너지 (0되면 지침)
    public float currentHunger; // 현재 배고픔 (0되면 파업)
    public float affection;     // 친밀도 (0 ~ 100)

    // ==========================================
    // 5. 성격과 기억 (Traits & Memories)
    // * 우리 게임의 핵심 '감성' 데이터
    // ==========================================
    public string personality;  // 기본 성격 (예: "겁쟁이", "대식가")

    // 후천적으로 얻은 특성/기억 리스트 (예: ["늑대의 공포", "따뜻한 스튜의 추억"])
    public List<string> memoryTraits = new List<string>();


    // ==========================================
    // [생성자] : 새로운 강아지를 처음 만들 때 호출
    // ==========================================
    public DogData(string name, int speed, int power, int stamina)
    {
        this.dogID = System.Guid.NewGuid().ToString(); // 랜덤한 고유 ID 생성
        this.dogName = name;
        this.age = 1;

        // 스탯 초기화
        this.statSpeed = speed;
        this.statPower = power;
        this.statStamina = stamina;

        // 상태 초기화 (꽉 찬 상태로 시작)
        this.currentEnergy = 100f;
        this.currentHunger = 100f;
        this.affection = 0f;
    }

    // ==========================================
    // [기능] : 기억(특성) 추가하기
    // ==========================================
    public void AddMemory(string memoryKey)
    {
        if (!memoryTraits.Contains(memoryKey))
        {
            memoryTraits.Add(memoryKey);
            Debug.Log($"[기록됨] {dogName}에게 새로운 기억이 생겼습니다: {memoryKey}");
        }
    }
}
