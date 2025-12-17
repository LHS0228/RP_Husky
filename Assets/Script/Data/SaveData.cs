using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // 여기에 저장하고 싶은 모든 걸 다 때려 넣으세요.
    public int playerGold; //소지금

    //가지고 있는 강아지 리스트
    public List<DogData> dogList = new List<DogData>();
}
