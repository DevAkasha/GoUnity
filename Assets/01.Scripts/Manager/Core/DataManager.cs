using System.Collections.Generic;

public class MonsterData : BaseData
{
    public int maxHp;
    public string name;
}

public class CharacterData : BaseData
{
    public int atk;
    public int powerShotCount;
}

public class ItemData : BaseData
{
    public string name;
    public string desc;
}

public class DataManager : Manager<DataManager>
{
    public Dictionary<string, BaseData> datas = new Dictionary<string, BaseData>();

    protected override void Awake()
    {
        base.Awake();
        datas.Add("Character", new CharacterData() { key = "Character", atk = 1, powerShotCount = 500 });
        datas.Add("Monster1", new MonsterData() { key = "Monster1", maxHp = 1000, name = "잡몹1" });
        datas.Add("Monster2", new MonsterData() { key = "Monster2", maxHp = 2000, name = "잡몹2" });
        datas.Add("Monster3", new MonsterData() { key = "Monster3", maxHp = 3000, name = "잡몹3" });
        datas.Add("Monster4", new MonsterData() { key = "Monster4", maxHp = 4000, name = "잡몹4" });
        datas.Add("Monster5", new MonsterData() { key = "Monster5", maxHp = 5000, name = "잡몹5" });
        datas.Add("Monster6", new MonsterData() { key = "Monster6", maxHp = 6000, name = "잡몹6" });
        datas.Add("Monster7", new MonsterData() { key = "Monster7", maxHp = 7000, name = "잡몹7" });
        datas.Add("Monster8", new MonsterData() { key = "Monster8", maxHp = 8000, name = "잡몹8" });
        datas.Add("Item1", new ItemData() { key = "Item1", name = "아이템1", desc = "아이템 그 첫번째" });
        datas.Add("Item2", new ItemData() { key = "Item2", name = "아이템2", desc = "아이템 그 두번째" });
        datas.Add("Item3", new ItemData() { key = "Item3", name = "아이템3", desc = "아이템 그 세번째" });
        datas.Add("Item4", new ItemData() { key = "Item4", name = "아이템4", desc = "아이템 그 네번째" });
        datas.Add("Item5", new ItemData() { key = "Item5", name = "아이템5", desc = "아이템 그 다섯번째" });
        datas.Add("Item6", new ItemData() { key = "Item6", name = "아이템6", desc = "아이템 그 여섯번째" });
        datas.Add("Item7", new ItemData() { key = "Item7", name = "아이템7", desc = "아이템 그 일곱번째" });
        datas.Add("Item8", new ItemData() { key = "Item8", name = "아이템8", desc = "아이템 그 여덟번째" });
        datas.Add("Item9", new ItemData() { key = "Item9", name = "아이템9", desc = "아이템 그 아홉번째" });
        datas.Add("Item10", new ItemData() { key = "Item10", name = "아이템10", desc = "아이템 그 열번째" });
    }

    public List<BaseData> GetMonsterDatas()
    {
        var datas = new List<BaseData>(this.datas.Values);
        return datas.FindAll(obj => obj.key.Contains("Monster"));
    }

    public T GetData<T>(string key) where T : BaseData
    {
        return (T)datas[key];
    }

    public List<BaseData> GetDatas<T>() where T : BaseData
    {
        var datas = new List<BaseData>(this.datas.Values);
        return datas.FindAll(obj => obj.GetType().IsAssignableFrom(typeof(T)));
    }
}
