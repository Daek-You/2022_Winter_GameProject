using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*   
 *   - ������ �빮�� ��� �κ� ���̾� �и��� �Ǽ��� ������ż�,
 *   - ���� ������ ���� �÷��̾��� ���̾ ��� �����ϴ� ������ ���� (�̷��� �� �ϸ�, �빮 ��պ��� ȭ�� �� ������ �Ѿ)
 *   - ����, ������ �빮, ������ ��, ���� �� ��� Order in Layer = 0
 *   - Sorting Layer�� ���� ���̾� ������ ���� �ο���
 */


public class GateWallLayerController : MonoBehaviour
{

    private string switChingLayerName = "HiddenArea";
    private Dictionary<GameObject, string> dic = new Dictionary<GameObject, string>();


    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject targetObject = collision.gameObject;
        SpriteRenderer[] renderers = collision.gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (renderers != null && collision.gameObject != null)
        {
            string originName = renderers.Length > 0 ? renderers[0].sortingLayerName : null;  // �� �� ���� ����� ������? ������ ���� ���� �� ������..
            if (originName != null)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.sortingLayerName = switChingLayerName;
                }
                dic.Add(targetObject, originName);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject targetObject = collision.gameObject;
        SpriteRenderer[] renderers = collision.gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (renderers != null && targetObject != null)
        {
            if (dic.ContainsKey(targetObject))
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.sortingLayerName = dic[targetObject];
                }
                dic.Remove(targetObject);
            }
        }
    }
}
