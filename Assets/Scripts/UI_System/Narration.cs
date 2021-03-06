using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System.Text;     // StringBuilder



public class Narration : MonoBehaviour
{

    public Text text;
    private const string jsonFilePath = "Json/Narration";

    private JsonData _jsonData;
    private int index;
    private StringBuilder _jsonStringBD = new StringBuilder();
    private WaitForSeconds delayTime = new WaitForSeconds(0.08f);
    private WaitForSeconds commaDelayTime = new WaitForSeconds(1.5f);
    private WaitForSeconds loadingDelay = new WaitForSeconds(2f);
    private string jsonDataLine;
    private const string nextIngameSceneName = "Village_Scene";


    void Awake()
    {
        _jsonData = ReadJsonFile();
        index = 0;
    }


    public void ShowNarration(bool restingComma)
    {
        if(_jsonData == null)
        {
            Debug.Log("파일을 읽어오지 못했습니다.");
        }


        if(_jsonData != null && index < _jsonData[0].Count)
        {
            text.text = "";
            _jsonStringBD.Clear();
            jsonDataLine = _jsonData[0][index].ToString();
            StartCoroutine(TextTyping(restingComma));
            index++;
        }
    }



    private IEnumerator TextTyping(bool restingComma)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

        foreach (char i in jsonDataLine)
        {
            if(i == ',' && restingComma)
            {
                text.text = _jsonStringBD.Append(i).ToString();
                yield return commaDelayTime;
                continue;
            }

            text.text = _jsonStringBD.Append(i).ToString();
            yield return delayTime;
        }

        if(index == _jsonData[0].Count)
        {
            yield return loadingDelay;
            LoadingSceneManager.LoadScene(nextIngameSceneName);
        }
    }


    public void TextFadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);

        while (text.color.a > 0f)
        {
            float alphaValue = text.color.a - (Time.deltaTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alphaValue);
            yield return null;
        }
    }



    private JsonData ReadJsonFile()
    {

        var jsonTextFile = Resources.Load<TextAsset>(jsonFilePath); // Resource 폴더에 있는 JSON 파일을 로드해줘야함
        JsonData jsonData = JsonMapper.ToObject(jsonTextFile.ToString());
        //if(File.Exists(Application.dataPath + jsonFilePath))
        //{
        // string jsonString = File.ReadAllText(Application.dataPath + jsonFilePath);
        //    JsonData jsonData = JsonMapper.ToObject(jsonString);
        //    return jsonData;
        //}
        //return null;
        return jsonData;

    }
}
