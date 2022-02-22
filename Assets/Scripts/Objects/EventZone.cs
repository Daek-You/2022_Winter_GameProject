using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{

    public bool isOneTimeEvent;
    private bool checkSwitch = false;
    private string objectTag;

    void Start()
    {
        objectTag = this.gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isOneTimeEvent && !checkSwitch)  // ��ȸ�� �̺�Ʈ�� ���
            {
                ///Text.Mananger���� �ؽ�Ʈ ��� ��û
                DialogCont.Instance.ShowDialog(tag);
                checkSwitch = true;
            }

            else if (!isOneTimeEvent)            // ��ȸ�� �̺�Ʈ
            {
                
            }

        }
    }
}
