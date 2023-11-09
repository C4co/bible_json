using System.IO.Compression;
using System.Text.Json;

using BibleJson.Domain;

namespace BibleJson.Test;

[TestClass]
public class BibleGzTest
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
            using var enFileStream = File.OpenRead("../../../../books/bible_en.gz");
            using var enGzipStream = new GZipStream(enFileStream, CompressionMode.Decompress);
            _en_bible = JsonSerializer.Deserialize<Bible>(enGzipStream)!;

            using var ptFileStream = File.OpenRead("../../../../books/bible_pt-br.gz");
            using var ptGzipStream = new GZipStream(ptFileStream, CompressionMode.Decompress);
            _pt_bible = JsonSerializer.Deserialize<Bible>(ptGzipStream)!;

            using var esFileStream = File.OpenRead("../../../../books/bible_es.gz");
            using var esGzipStream = new GZipStream(esFileStream, CompressionMode.Decompress);
            _es_bible = JsonSerializer.Deserialize<Bible>(esGzipStream)!;

            using var frFileStream = File.OpenRead("../../../../books/bible_fr.gz");
            using var frGzipStream = new GZipStream(frFileStream, CompressionMode.Decompress);
            _fr_bible = JsonSerializer.Deserialize<Bible>(frGzipStream)!;

            using var itFileStream = File.OpenRead("../../../../books/bible_it.gz");
            using var itGzipStream = new GZipStream(itFileStream, CompressionMode.Decompress);
            _it_bible = JsonSerializer.Deserialize<Bible>(itGzipStream)!;
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
    public void BibleLang()
    {
        Assert.AreEqual("en", _en_bible!.Lang);
        Assert.AreEqual("pt-br", _pt_bible!.Lang);
        Assert.AreEqual("es", _es_bible!.Lang);
        Assert.AreEqual("fr", _fr_bible!.Lang);
        Assert.AreEqual("it", _it_bible!.Lang);
    }
}
