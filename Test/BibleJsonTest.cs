using System.Text.Json;

using BibleJson.Domain;

namespace BibleJson.Test;

[TestClass]
public class BibleTest
{
    Bible? _en_bible;
    Bible? _pt_bible;
    Bible? _es_bible;
    Bible? _fr_bible;
    Bible? _it_bible;

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

            _es_bible = JsonSerializer.Deserialize<Bible>(
                File.ReadAllText("../../../../books/bible_es.json")
            )!;

            _fr_bible = JsonSerializer.Deserialize<Bible>(
                File.ReadAllText("../../../../books/bible_fr.json")
            )!;

            _it_bible = JsonSerializer.Deserialize<Bible>(
                File.ReadAllText("../../../../books/bible_it.json")
            )!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [TestMethod]
    public void BibleExistsTest()
    {
        Assert.IsNotNull(_en_bible);
        Assert.IsNotNull(_pt_bible);
        Assert.IsNotNull(_es_bible);
        Assert.IsNotNull(_fr_bible);
        Assert.IsNotNull(_it_bible);
    }

    [TestMethod]
    public void BookCountTest()
    {
        Assert.AreEqual(66, _en_bible!.Books.Count);
        Assert.AreEqual(66, _pt_bible!.Books.Count);
        Assert.AreEqual(66, _es_bible!.Books.Count);
        Assert.AreEqual(66, _fr_bible!.Books.Count);
        Assert.AreEqual(66, _it_bible!.Books.Count);
    }

    [TestMethod]
    public void BibleLangTest()
    {
        Assert.AreEqual("en", _en_bible!.Lang);
        Assert.AreEqual("pt-br", _pt_bible!.Lang);
        Assert.AreEqual("es", _es_bible!.Lang);
        Assert.AreEqual("fr", _fr_bible!.Lang);
        Assert.AreEqual("it", _it_bible!.Lang);
    }
}