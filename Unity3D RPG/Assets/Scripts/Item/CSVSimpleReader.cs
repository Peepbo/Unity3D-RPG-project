using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace CSVSimpleReader
{
    public class CSVSimpleReader
    {
        //CSV
        public static List<string> Reader(string fileName, int startRow = 0, int endRow = 0)
        {
            List<string> _playerSaveInfo = new List<string>();

            TextAsset _playerData = Resources.Load(fileName) as TextAsset;
            string[] _rowData = _playerData.text.Split(new char[] { '\n' });
            if (endRow == 0)
                endRow = _rowData.Length;

            for (int i = startRow; i < endRow; i++)
            {
                string[] _column = _rowData[i].Split(new char[] { ',' });

                for (int k = 0; k < _column.Length; k++)
                {
                    _playerSaveInfo.Add(_column[k]);
                }
            }

            return _playerSaveInfo;
        }
    }
}

//        //   //List<ItemInfo> itemList = new List<ItemInfo>();

//        //    //// TSV : 웹에껄 다운받아서 정보를 가져오는 기능
//        //    //const string URL = "https://docs.google.com/spreadsheets/d/1YuynEfJHxImtJK0brB68n2lLriSeY-y_O9WkFxoI1Wg/export?format=tsv&range=A2:E";
//        //    //IEnumerator Start()
//        //    //{
//        //    //    UnityWebRequest webLink = UnityWebRequest.Get(URL);
//        //    //    yield return webLink.SendWebRequest();

//        //    //    string data = webLink.downloadHandler.text;

//        //    //    string[] dataArray = data.Split(new char[] { '\n' });
//        //    //    for (int i = 0; i < dataArray.Length; i++)
//        //    //    {
//        //    //        string[] row = dataArray[i].Split(new char[] { '\t' });
//        //    //        ItemInfo itemDB = new ItemInfo();

//        //    //        //int.TryParse(row[1], out itemDB.id);// 문자열이 숫자인지 확인하고 처리하는 방식
//        //    //        itemDB.id = int.Parse(row[(int)ITEMLIST.ID]); // 문자열이 숫자가 아니여도 담는방식 효율이 좀더 좋음
//        //    //        itemDB.kind = row[(int)ITEMLIST.KIND];
//        //    //        itemDB.itemName = row[(int)ITEMLIST.NAME];
//        //    //        itemDB.atk = int.Parse(row[(int)ITEMLIST.ATK]);
//        //    //        itemDB.def = int.Parse(row[(int)ITEMLIST.DEF]);

//        //    //        itemList.Add(itemDB);
//        //    //    }

//        //    //    foreach (ItemInfo a in itemList)
//        //    //    {
//        //    //        Debug.Log("id : " + a.id + "kind : " + a.kind + "name : " + a.itemName + "atk : " + a.atk + "def : " + a.def);
//        //    //    }
//        //    //}
//    }
//}
