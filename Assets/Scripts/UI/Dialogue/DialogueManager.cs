using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    private string googleSheetUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ-7AJkm-U8HSL58odTd349sc_qm0acaqva8HqZkNOBB2fZ-ZY2KZMR5ShIuPE2sQ/pub?gid=1391226435&single=true&output=csv";
    public List<DialogueData> dialogueData = new List<DialogueData>();
    public static DialogueManager instance;
    private void Awake() 
    {
        instance = this;
        StartCoroutine(LoadQuestTableSheet());  
    }
    
    private IEnumerator LoadQuestTableSheet()
    {
        yield return FetchGoogleSheet(googleSheetUrl, ParseQuestTableCSV);
    }
    private IEnumerator FetchGoogleSheet(string url, System.Action<string> onSuccess)
    {
        int retries = 3;
        while (retries > 0)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
                yield break;
            }
            else
            {
                Debug.LogError($"스프라이트 시트 로드 실패 {url}: {request.error}");
                retries--;
                if (retries > 0)
                {
                    Debug.Log("Retrying...");
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    Debug.LogError("로드 실패");
                }
            }
        }
        
    }
    private void ParseQuestTableCSV(string csvData)
    {
        string[] rows = csvData.Split('\n');
        
        for (int i = 1; i < rows.Length; i++) // Skip header row
        {
            string[] cells = rows[i].Split(',');
            if (cells.Length < 3) continue; // Skip malformed rows
            DialogueData dialogue = new DialogueData
            {
                dialogue_index = int.Parse(cells[0]),
                dialogue_context = ParseStringArray(cells[1]),
                dialogue_text = ParseStringArray(cells[2])
            };
            dialogueData.Add(dialogue);
            
        }
    }
    private string[] ParseStringArray(string rawString)
    {
        rawString = rawString.Trim();

        if (rawString.StartsWith("[") && rawString.EndsWith("]"))
        {
            rawString = rawString.Substring(1, rawString.Length - 2);
        }

        return rawString.Split('/')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s)) // Remove empty entries
                    .ToArray();
    }

}

