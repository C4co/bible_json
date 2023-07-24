# Bible JSON

Script to extract content from [Biblia online](https://www.bibliaonline.com.br/vc)
and transform to Json.

#### Format
```
{
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

---

@ Carlos Costa - 2023