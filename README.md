# DatabaseComparer

**DatabaseComparer** is a C# console application that compares data between two SQL Server databases with the same schema. It highlights added, deleted, and modified rows in a detailed HTML report.

---

## 🚀 Features

- Compares selected tables between two databases.
- Detects:
  - 🔄 Modified rows (highlighted in yellow)
  - ➕ Added rows in DB B (highlighted in green)
  - ➖ Deleted rows from DB A (highlighted in red)
- Outputs a clean, color-coded `report.html`.

---

## ⚙️ Configuration

Edit `appsettings.json`:

```json
{
  "DatabaseA": {
    "ConnectionString": "Server=localhost;Database=MyDbA;Trusted_Connection=True;"
  },
  "DatabaseB": {
    "ConnectionString": "Server=localhost;Database=MyDbB;Trusted_Connection=True;"
  },
  "TablesToCompare": [
    "Customers",
    "Orders",
    "Products"
  ]
}
```

---

## 🏁 Running the App

```bash
dotnet build
dotnet run
```

After execution, open `report.html` in your browser to view the comparison results.

---

## 📝 License

This project is licensed under the [MIT License](LICENSE).

---

## 🙋‍♂️ Contributions

Feel free to fork this repository and submit pull requests for improvements such as:

* UI enhancements in HTML output
* Support for custom primary keys
* Command-line arguments
* Different database engines

---

## 📧 Contact

Questions? Ideas? Reach out via [GitHub Issues](https://github.com/faridprogrammer/database-comparer/issues).
