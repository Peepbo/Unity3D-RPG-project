using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

partial class PlayerData
{
    public void SaveChest(int itemNumber)
    {
        ItemInfo _item = CSVData.Instance.find(itemNumber);

        //없으면 ?
        if (myItem.Contains(_item) == false) myItem.Add(_item);
        //if (myItem.ContainsKey(itemNumber) == false) myItem[itemNumber] = 1;

        //있으면?
        else
        {
            int _index = myItem.IndexOf(_item);
            myItem[_index].count++;
        }
        //else myItem[itemNumber] += 1;

        //SaveData();
    }

    public void SaveAbility(List<StatInfo> list)
    {
        for (int i = 0; i < 35; i++)
        {
            if (list[i].isLearn)
                myAbility[i] = 1;
            else
                myAbility[i] = 0;
        }

        SaveData();
    }
}