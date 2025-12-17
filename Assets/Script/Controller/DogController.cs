using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField, Header("강아지 개인 데이터")]
    public DogData myData;

    [SerializeField, Header("강아지 외견")]
    public SpriteRenderer mainBodySprite;
    public SpriteRenderer subBodySprite;
    public SpriteRenderer eyesSprite;
    public GameObject patternObj;

    // [중요] 외부(매니저)에서 데이터를 꽂아주는 함수
    public void Initialize(DogData data)
    {
        this.myData = data; // "아, 나는 이제부터 이 데이터(바둑이)대로 행동한다."

        // 3. 받은 데이터대로 외형 업데이트
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        // 데이터에 적힌 '색깔 번호'를 보고 실제 색을 입힘
        // (색상 리스트는 매니저나 별도 DB에서 가져온다고 가정)
        // bodySprite.color = ColorManager.GetColor(myData.bodyColorIndex);

        // 무늬가 있다고 데이터에 적혀있으면 켬
        patternObj.SetActive(myData.hasPattern);

        // 이름표 갱신 등...
        gameObject.name = myData.dogName;
    }

    // 4. 행동 (밥 먹기, 달리기 등)
    // * 행동을 하면 데이터(영혼)를 수정합니다.
    public void EatFood()
    {
        // 내 뱃속(데이터)의 허기 수치를 채움
        myData.currentHunger += 30;

        // 데이터가 바뀌었으니 저장하라고 매니저에게 신호
        //SaveManager.Instance.SaveGame();
    }
}
