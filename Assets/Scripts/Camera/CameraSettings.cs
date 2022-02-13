using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    public Transform followingTarget;
    public float followingSpeed;
    private float cameraPositionY;


    void Awake()
    {
        cameraPositionY = this.transform.position.y;
    }


    void LateUpdate()
    {
        FollowTarget();
    }


    private void FollowTarget()
    {
        // ī�޶��� Z ��ġ���� ���� (���� ������Ʈ���� �� �� �ʿ� �־�� �������� ���δ�.)
        Vector3 targetPosition = new Vector3(followingTarget.position.x,
                                           cameraPositionY,
                                           -10f);
        transform.position = Vector3.Lerp(transform.position,
                                          targetPosition,
                                          followingSpeed * Time.deltaTime);
    }
}
