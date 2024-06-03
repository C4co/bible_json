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

### Showcase

Check the app on Google Play Store: https://play.google.com/store/apps/details?id=cnc.holy_bible_digital

![image](https://cdn.dribbble.com/userupload/14599209/file/original-ea4bc9775217335dd31996eba5d45442.png?resize=752x)


---

@ Carlos Costa - 2023
