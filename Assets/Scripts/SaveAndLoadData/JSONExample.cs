using UnityEditor.PackageManager.UI;
using UnityEngine;

public class JSONExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SampleData sample = new SampleData();
        sample.name = "Dave";
        sample.score = -5;

        string data = JsonUtility.ToJson(sample);
        Debug.Log(data);

        // Deserialization
        string jsonString = "{\n\t\"name\": \"Alice\",\n\t\"score\": 90.34\n}";
        SampleData sample2 = JsonUtility.FromJson<SampleData>(jsonString);
        Debug.Log($"Deserialized Name: {sample2.name}, Score: {sample2.score}");
    }  

}
