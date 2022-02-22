using System.Collections;
using System.Collections.Generic;
using ObjectState;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    public SpriteRenderer hidingSprite;
    public bool isDynamicHiddenSpace;        // ��ȣ�ۿ� ��û�� ������ �÷��̾� ��ġ�� ���� ���
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
                /// TextMangaer���� �±� �̸��� ���ڷ� �Ͽ� ��� ��û
                DialogCont.Instance.ShowDialog(objectTag);

                break;

            case (int)ObjectType.eCanHideObject:
                if(hidingSprite != null)
                    suji.Hide(hidingSprite, isDynamicHiddenSpace);

                break;

            case (int)ObjectType.eItemObject:
                /// GameDataManager ������Ʈ�� ����, �� ������Ʈ�� ������ �����͸� �����Ͽ� ����
                /// ����, �̱��� �������� �����ϸ� ���� ��
                /// ex) ���踦 �Ա� �� : key = 0     /  ���� ���� �� : key = 1
                
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
