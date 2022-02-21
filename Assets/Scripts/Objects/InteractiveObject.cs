using System.Collections;
using System.Collections.Generic;
using ObjectState;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    public SpriteRenderer hidingSprite;
    public bool isDynamicHiddenSpace;        // 상호작용 요청한 시점의 플레이어 위치에 숨을 경우
    public ObjectType objectType;
    private SujiController suji;
    private string objectTag;


    void Start()
    {
        objectTag = this.gameObject.tag;
    }


    public void Interaction()
    {

        switch ((int)objectType)
        {
            case (int)ObjectType.eSimpleEventObject:
                /// TextMangaer에게 태그 이름을 인자로 하여 출력 요청
                
                break;

            case (int)ObjectType.eCanHideObject:
                if(hidingSprite != null)
                    suji.Hide(hidingSprite, isDynamicHiddenSpace);

                break;

            case (int)ObjectType.eItemObject:
                /// GameDataManager 오브젝트를 만들어서, 이 오브젝트의 아이템 데이터를 전달하여 저장
                /// 역시, 싱글톤 패턴으로 구현하면 좋을 듯
                /// ex) 열쇠를 먹기 전 : key = 0     /  열쇠 먹은 후 : key = 1
                
                break;

            case (int)ObjectType.eDoorObject:
                AbstractGate gate = GetComponentInChildren<AbstractGate>();

                if (gate != null)
                    gate.OnEvent(objectTag);

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            suji = collision.gameObject.GetComponent<SujiController>();

            if (suji != null)
            {
                suji.InteractiveObject = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (suji != null)
            {
                suji.InteractiveObject = null;
                suji = null;
            }
        }
    }
}
