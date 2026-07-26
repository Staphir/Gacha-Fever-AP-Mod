using GachaFeverAP.Utils;

namespace GachaFeverAP.Tests.Utils;

public class DataSaveIOTest
{
    public DataSaveIOTest()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "MockFiles", "DataSaveMock.json");
        MockJson = File.ReadAllText(path);
    }

    private string MockJson { get; }

    [Fact]
    public void TestGetUnlockGachas()
    {
        var expected = new List<int>
        {
            (int)Constants.BaseId.Gacha,
            (int)Constants.BaseId.Gacha + 16,
            (int)Constants.BaseId.Gacha + 17,
            (int)Constants.BaseId.Gacha + 18,
            (int)Constants.BaseId.Gacha + 20
        };
        var result = DataSaveIO.GetUnlockGachas(MockJson);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestGetUnlockWeapons()
    {
        var expected = new List<int>
        {
            (int)Constants.BaseId.Weapon + 10,
            (int)Constants.BaseId.Weapon + 11,
            (int)Constants.BaseId.Weapon + 23,
            (int)Constants.BaseId.Weapon + 24
        };
        var result = DataSaveIO.GetUnlockWeapons(MockJson);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestGetUnlockObjects()
    {
        var expected = new List<int>
        {
            (int)Constants.BaseId.Object + 8,
            (int)Constants.BaseId.Object + 9,
            (int)Constants.BaseId.Object + 12,
            (int)Constants.BaseId.Object + 21,
            (int)Constants.BaseId.Object + 22
        };
        var result = DataSaveIO.GetUnlockObjects(MockJson);
        Assert.Equal(expected, result);
    }
}