using System.Collections.Generic;

[System.Serializable]
public class BillData
{
    public string time;
    public List<BillItemData> items = new();
    public int total;
}

[System.Serializable]
public class BillItemData
{
    public string itemName;
    public int quantity;
    public int price;
}
