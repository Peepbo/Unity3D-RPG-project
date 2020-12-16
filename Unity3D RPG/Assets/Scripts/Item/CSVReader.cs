using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;

namespace CSVReader
{
    public static class Reader
    {
        static string WordSplit = ",";
        static string LineSplit = @"\r\n|\n\r|\n|\r";
        static string FileSplit = @"/";

        //-------------------------------------------------------------------------------------------------
        public static Table ReadCSVToTable(string filePath)
        {
            Table result = null;

            // {{ FileName ~ 
            string fileName = "";
            string[] arraySplitFilePath = Regex.Split(filePath, FileSplit);
            if(arraySplitFilePath != null)
            {
                fileName = arraySplitFilePath[arraySplitFilePath.Length - 1];
            }
            // }} 

            //CSV파일 불러오기 => TextAsset으로 불러 오려면 반드시 meta데이터가 생성되어 있어야 함
            TextAsset textAsset = Resources.Load(filePath) as TextAsset;
            //전체 데이터를 줄단위로 나누자
            string[] arrayLineData = Regex.Split(textAsset.text, LineSplit);
            //데이터는 Header + value + 공란줄(마지막) 로 되어 있으므로 최소 2줄이상 있어야 정상적인 파일임
            if(arrayLineData != null && arrayLineData.Length >= 3)
            {
                //헤더 데이터 불러온다. (데이터 타입은 1행에 정의)
                string[] arrayHeader = Regex.Split(arrayLineData[0], WordSplit);
                if(arrayHeader != null && arrayHeader.Length > 0)
                {
                    //행에 있는 총 데이터 수
                    int rowCount = arrayLineData.Length - 2;
                    int columnCount = arrayHeader.Length;
                    //행렬 데이터
                    string[,] arrayData = new string[rowCount, columnCount];
                    //LineSplit으로 나누면 마지막 줄에 빈 공란이 딸려옴 => -1계산
                    for (int row = 1; row < arrayLineData.Length - 1; ++row)
                    {
                        //해당 라인 데이터 배열로 분리
                        string[] tempLine = Regex.Split(arrayLineData[row], WordSplit);
                        //데이터 세팅
                        for(int column = 0; column < tempLine.Length; ++column)
                        {
                            arrayData[row - 1, column] = tempLine[column];
                        }
                    }
                    //테이블 생성
                    result = new Table(fileName, arrayData,arrayHeader, rowCount, columnCount);
                }
            }

            return result;
        }

    }
}
