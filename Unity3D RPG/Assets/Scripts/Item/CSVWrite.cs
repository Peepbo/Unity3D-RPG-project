using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace CSVWrite
{
    public static class Write
    {
        public static void PlayerSave(int money,List<int> equip, List<ItemInfo> storage, List<string> ability, string filePath)
        {
            string _storageStr="";
            string _abilityStr="";
            for(int i=0; i<storage.Count; i++)
            {
                _storageStr += storage[i].id.ToString() + ','+ storage[i].count.ToString();
                if (i != storage.Count - 1)
                    _storageStr += ',';
            }
               
            for(int i=0; i<ability.Count;i++)
            {
                if (i != (ability.Count - 1))
                    _abilityStr += ability[i] + ',';
                else
                    _abilityStr += ability[i];
            }
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filePath,false))
            {
                file.Write(money.ToString());
                for(int i=0; i<equip.Count; i++)             { file.Write("," + equip[i].ToString()); }
                file.WriteLine('\n'+_storageStr);
                file.Write(_abilityStr);
            }

            Resources.Load(filePath);
            //AssetDatabase.ImportAsset(@filePath);
        }
    }
}
