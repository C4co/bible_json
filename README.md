# Bible JSON

[![.NET](https://github.com/C4co/bible_json/actions/workflows/dotnet.yml/badge.svg)](https://github.com/C4co/bible_json/actions/workflows/dotnet.yml)

Extract content from [Biblia online](https://www.bibliaonline.com.br/vc)
and transform to Json and gzip.

| Langs              | json | gzip |
| ------------------ | ---- | ---- |
| Portuguese - pt-br | ✅    | ✅    |
| English - en       | ✅    | ✅    |
| Spanish - es       | ✅    | ✅    |
| Italian - it       | ✅    | ✅    |
| French - fr        | ✅    | ✅    |

### Json Format
```js
{
  lang: string
  books: {
    [
      {
        name: string,
        link: string,
        abbrev: string,
        testament: number
        chapters: [[string]]
      }
    ]
  }
}
```

### Run

```
dotnet run --project App
```

### Tests

```
dotnet run test
```

---

@ Carlos Costa - 2023
