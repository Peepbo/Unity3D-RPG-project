using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSVReader
{

    //-------------------------------------------------------------------------------------------------
    //Dictionary에 담을 데이터중에 명식적으로 키값을 세팅하고 싶다면 해당 Attribute를 달아준다
    public class DataAttribute : System.Attribute
    {
        public static readonly DataAttribute Default = new DataAttribute();

        public string KeyName { get; set; }

        //-------------------------------------------------------------------------------------------------
        public DataAttribute(string keyName = null)
        {
            KeyName = keyName;
        }
    }


    //-------------------------------------------------------------------------------------------------
    public static class KeySelector
    {
        //해당 데이터 Attribute 조사해서 Key변수이름 찾아온다
        public static string TryGetKey<TData>() where TData : class, new()
        {
            System.Type type = typeof(TData);
            //어트리부트 불러온다. 단,데이터는 상속을 안쓰니까 herit false로
            object[] arrayAttribute = type.GetCustomAttributes(typeof(CSVReader.DataAttribute), false);

            if (arrayAttribute == null)
                return string.Empty;

            DataAttribute attribute = arrayAttribute[0] as DataAttribute;

            string result = attribute.KeyName;

            if(result != null)
            {
                return result;
            }
            //어트리부트 지정이 안되어 있다면 변수중에 가장 상단에 있는 변수를 키로 사용하자
            else
            {
                System.Reflection.FieldInfo[] arrayFieldInfo = type.GetFields();
                return arrayFieldInfo[0].Name;
            }
        }
    }
}
