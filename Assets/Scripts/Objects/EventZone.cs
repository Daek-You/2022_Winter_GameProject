using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{

    public bool isOneTimeEvent;
    private bool checkSwitch = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isOneTimeEvent && !checkSwitch)  // ��ȸ�� �̺�Ʈ�� ���
            {
                ///Text.Mananger���� �ؽ�Ʈ ��� ��û
                checkSwitch = true;
            }

            else if (!isOneTimeEvent)            // ��ȸ�� �̺�Ʈ
            {
                
            }

        }
    }
}
