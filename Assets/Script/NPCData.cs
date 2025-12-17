using UnityEngine;

public class NPCData : ScriptableObject
{
    [Header("기본 정보")]
    public string npcName; //이름
    public Sprite portrait; //포스터, 대화창 사진

    [Header("Ink 스토리 개인 파일 데이터")]
    public TextAsset inkJSONAsset;
}
