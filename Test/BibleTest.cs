using System.Text.Json;

using BibleJson.Domain;

namespace BibleJson.Test;

[TestClass]
public class BibleTest
{
    Bible? _en_bible;
    Bible? _pt_bible;

    [TestInitialize]
    public void Setup()
    {
        try
        {
            _en_bible = JsonSerializer.Deserialize<Bible>(
                File.ReadAllText("../../../../books/bible_en.json")
            )!;

            _pt_bible = JsonSerializer.Deserialize<Bible>(
                File.ReadAllText("../../../../books/bible_pt-br.json")
            )!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void CheckBibleTest()
    {
        Assert.IsNotNull(_en_bible);
        Assert.IsNotNull(_pt_bible);
    }

    [TestMethod]
    public void CheckBooksTest()
    {
        Assert.AreEqual(66, _en_bible!.Books.Count);
        Assert.AreEqual(66, _pt_bible!.Books.Count);
    }

    [TestMethod]
    public void CheckLangTest()
    {
        Assert.AreEqual("en", _en_bible!.Lang);
        Assert.AreEqual("pt-br", _pt_bible!.Lang);
    }
}