using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{

    public GameObject rightDoorAxis;
    public GameObject leftDoorAxis;
    public GameObject amulet;
    public Collider2D gateGoalKeeper;
    public float rotationSpeed;
    public bool controlSignal;

    private bool isGateOpen = false;
    private const float maxRotationValue = 90f;
    private const float minRotationValue = 0f;
    private const float FLOAT_COMPARISON_VALUE = 2f;
    private WaitForSeconds gatePassWaitTime = new WaitForSeconds(0.5f);


    void Update()
    {
        if (!isGateOpen && controlSignal)
        {
            StartCoroutine(GateOpen());
        }

        else if (isGateOpen && controlSignal)
        {
            StartCoroutine(GateClose());
        }

    }

    IEnumerator GatePassDelay(bool isOpening)
    {
        yield return gatePassWaitTime;

        if (isOpening)
            gateGoalKeeper.gameObject.SetActive(false);
        else
            gateGoalKeeper.gameObject.SetActive(true);
    }

    IEnumerator GateClose()
    {
        controlSignal = false;
        amulet.SetActive(true);  // ������ ��� ó���ؾ��� �� ���� �� ���ؼ� Ȱ��ȭ/��Ȱ��ȭ ������� �ϴ� �ص�
        StartCoroutine(GatePassDelay(false));

        while (true)
        {
            float rDoorCurEulerY = rightDoorAxis.transform.eulerAngles.y;
            float lDoorCurEulerY = leftDoorAxis.transform.eulerAngles.y;
            bool isCompleteClose = _Approximately(rDoorCurEulerY, minRotationValue) && _Approximately(lDoorCurEulerY, minRotationValue);

            if (isCompleteClose)
                break;

            rightDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(rDoorCurEulerY, minRotationValue, Time.deltaTime * rotationSpeed), 0f);
            leftDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(lDoorCurEulerY, minRotationValue, Time.deltaTime * rotationSpeed), 0f);
            yield return null;
        }

        isGateOpen = false;
    }

    IEnumerator GateOpen()
    {
        controlSignal = false;
        amulet.SetActive(false);  // ������ ��� ó���ؾ��� �� ���� �� ���ؼ� �켱 �� ���� �� ��� ��Ȱ��ȭ�ص�
        StartCoroutine(GatePassDelay(true));

        while (true)
        {
            float rDoorCurEulerY = rightDoorAxis.transform.eulerAngles.y;
            float lDoorCurEulerY = leftDoorAxis.transform.eulerAngles.y;
            bool isCompleteOpen = _Approximately(rDoorCurEulerY, maxRotationValue) && _Approximately(lDoorCurEulerY, maxRotationValue);

            if (isCompleteOpen)
                break;

            rightDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(rDoorCurEulerY, maxRotationValue, Time.deltaTime * rotationSpeed), 0f);
            leftDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(lDoorCurEulerY, maxRotationValue, Time.deltaTime * rotationSpeed), 0f);
            yield return null;
        }

        isGateOpen = true;
    }


    /// <summary>
    /// Mathf.Approximately�� ����, Mathf.LerpAngle �Լ� Ư���� ��ǥ���� ����������� �������� �۾����� ������
    /// ���� �������� �ʹ� ����� �ð��� �ɸ�
    /// ex) 90������ �����ٸ�, 89.90030, 89.90031, ... �̷� ������ �ʹ� ������ ������
    /// �׷��� "threshold ��"(FLOAT_COMPARISON_VALUE)�� �ϳ� �����ϰ�, ���� �Լ��� �������
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool _Approximately(float x, float y)
    {
        float absCalcValue = (x - y) > 0f ? x - y : y - x;
        if (absCalcValue <= FLOAT_COMPARISON_VALUE)
            return true;
        return false;
    }
}
