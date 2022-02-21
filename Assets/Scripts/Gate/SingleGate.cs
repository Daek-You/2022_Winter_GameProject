using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGate : AbstractGate
{

    public GameObject doorAxis;


    protected override IEnumerator Open()
    {
        if(doorAxis != null)
        {
            StartCoroutine(GatePassDelay());


            while (true)
            {
                float currentDoorEulerY = doorAxis.transform.eulerAngles.y;
                bool isCompleteOpen = _Approximately(currentDoorEulerY, MAX_ANGLE);

                if (isCompleteOpen)
                    break;

                doorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(currentDoorEulerY, MAX_ANGLE, Time.deltaTime * rotateSpeed), 0f);
                yield return null;
            }

            _isOpen = true;
        }
    }

    public override void OnEvent(string tag)
    {
        switch (tag)
        {
            case "SujiHouseDoor":
                /// "�� �������� ��. ������ �� ���� �ƴϾ�" �ؽ�Ʈ ��� ��û
                break;

            case "SujiHouseGate":
                if (!_isOpen)
                {
                    /// "���� ����غ���." �ؽ�Ʈ ��� ��û
                    StartCoroutine(Open());
                }
                break;

            case "Shaman_KitchenDoor":
                StartCoroutine(Open());


                break;

            case "Shaman_MainRoomDoor":
                StartCoroutine(Open());

                break;

            case "Shaman_BathroomDoor":
                StartCoroutine(Open());

                break;
        }
    }
}