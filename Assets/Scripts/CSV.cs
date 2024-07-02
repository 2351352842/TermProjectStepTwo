using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEngine;


public class CSV : MonoBehaviour
{
    [HideInInspector]
    public string[] speeds;
    public static CSV instance;
    void Awake()
    {
        instance = this;
    }
    string separateSign = ",";
    string lineFeedSign = "\r\n";
    string path;
    // Start is called before the first frame update
    void Start()
    {
        speeds = new string[11];
        path = Application.dataPath + "/Resources/PlayerPropertyData.csv";
        CreateCSV();
        LoadCSV();
    }

    public void CreateCSV()
    {
        DataTable dataTable = new DataTable("PlayData");
        dataTable.Columns.Add("Level");
        dataTable.Columns.Add("Power");
        dataTable.Columns.Add("Speed");
        for (int i = 0; i < 10; i++)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = i;
            dataRow[1] = 50 * i;
            dataRow[2] = 2 * i + 1 * 1.13;
            dataTable.Rows.Add(dataRow);
        }
        StringBuilder csvString = new StringBuilder();


        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            csvString.Append(dataTable.Columns[i].ColumnName);
            if (i < dataTable.Columns.Count - 1)
            {
                csvString.Append(separateSign);
            }
        }
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            csvString.Append(lineFeedSign);
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                csvString.Append(dataTable.Rows[i][j].ToString());
                if (j < dataTable.Rows.Count - 1)
                {
                    csvString.Append(separateSign);
                }
            }
        }
        File.WriteAllText(path, csvString.ToString());

    }
    public void LoadCSV()
    {
        string csvString = File.ReadAllText(path);
        string[] csvRowDatas = csvString.Split('\n');
        for (int i = 0; i < csvRowDatas.Length; i++)
        {
            string[] csvDatas = csvRowDatas[i].Split(separateSign.ToCharArray());
            speeds[i] = csvDatas[2];
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

