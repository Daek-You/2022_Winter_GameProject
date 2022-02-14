using System.Collections;
using System.Collections.Generic;
using ObjectState;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    public ObjectType objectType;
    private SujiController suji;
    private string objectTag;


    void Start()
    {
        objectTag = this.gameObject.tag;        // ���ڿ��� ���۷��� Ÿ���̱� ������, ���� �� �������� �����ȴ�. �׷��� �����س��� ����.
    }


    public void Interaction()
    {
        if (objectType == ObjectType.eCanHideObject)
        {
            suji.Hide();
        }

        else if(objectType == ObjectType.eTextPrintObject)
        {
            /// TextManager���� �� ��ȣ�ۿ� ������Ʈ�� "objectTag"�� �Ű������� �ؼ� ����
            /// ex) TextManager.ShowDialog(string objectTag);
            /// TextManager������ �� �±׸� key�� �Ͽ�, �ű⿡ �����Ǵ� �ؽ�Ʈ(value)�� �������� ��.
            /// �̱���(Singleton) �������� �����ϸ� ���� ���ϴ�.
        }

        else if(objectType == ObjectType.eItemObject)
        {
            /// GameDataManager ������Ʈ�� ����, �� ������Ʈ�� ������ �����͸� �����Ͽ� ����
            /// ����, �̱��� �������� �����ϸ� ���� ��
            /// ex) ���踦 �Ա� �� : key = 0     /  ���� ���� �� : key = 1
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