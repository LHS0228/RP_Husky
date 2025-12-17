using UnityEngine;

public class NPC : EventObjectBase
{
    public NPCData data;

    public override void OnInteract(GameObject player)
    {
        //플레이어 보기
        LookAtPlayer(player);

        //대화 매니저에 ink 넘겨주기 요청
        DialogueManager.instance.StartDialogue(data.inkJSONAsset, data.portrait, data.npcName);
    }

    void LookAtPlayer(GameObject player)
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x > 0)
            transform.localScale = new Vector3(-1, 1, 1); // 오른쪽 봄 (스프라이트 뒤집기)
        else
            transform.localScale = new Vector3(1, 1, 1);  // 왼쪽 봄
    }
}
