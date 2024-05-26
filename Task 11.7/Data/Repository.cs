using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using Task_11._7.Model;

namespace Task_11._7.Data
{
    internal class Repository
    {
        public Repository() { }

        public void CreateNewFileXLSX(string filePathAndName)
        {
            //Создаем фаил excel
            IWorkbook workbook = new XSSFWorkbook();

            //Создаем новый лист
            ISheet sheet = workbook.CreateSheet("Clients");

            //Добавляем первую строку
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Last Name");
            headerRow.CreateCell(1).SetCellValue("First Name");
            headerRow.CreateCell(2).SetCellValue("Patronymic");
            headerRow.CreateCell(3).SetCellValue("Phone");
            headerRow.CreateCell(4).SetCellValue("Series and number pasport");

            //Сохраняем документ excel
            using (FileStream fileStream = new FileStream(filePathAndName, FileMode.Create))
            {
                workbook.Write(fileStream, false);
            }
        }

        public void AddNewClientInFileXLSX(string filePathAndName, Client client)
        {
            //Открытие существующей рабочей книги
            IWorkbook workbook;

            using (FileStream fileStream = new FileStream(filePathAndName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fileStream);
            }

            //Получение листа
            ISheet sheet = workbook.GetSheetAt(0);

            //Добавляем данные в ячейки
            IRow cells = sheet.CreateRow(1);
            cells.CreateCell(0).SetCellValue(client.LastName);
            cells.CreateCell(1).SetCellValue(client.FirstName);
            cells.CreateCell(2).SetCellValue(client.Patronymic);
            cells.CreateCell(3).SetCellValue(client.Phone);
            cells.CreateCell(4).SetCellValue(client.SeriesAndNumberPasport);

            //Сохраняем документ excel
            using (FileStream fileStream = new FileStream(filePathAndName, FileMode.Create))
            {
                workbook.Write(fileStream, false);
            }
        }

        public IEnumerable<Client> ReadFileXLSX(string filePathAndName)
        {
            IEnumerable<Client> clientsList = new List<Client>();

            //Открытие существующей рабочей книги
            IWorkbook workbook;

            using (FileStream fileStream = new FileStream(filePathAndName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fileStream);
            }

            //Получение листа
            ISheet sheet = workbook.GetSheetAt(0);

            //Чтение данных из ячейки

            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                var rowData = sheet.GetRow(row);

                if (rowData == null)
                {
                    continue;
                }
                Client readClient = new Client();



                readClient.FirstName = rowData.GetCell(0).StringCellValue;
                readClient.LastName = rowData.GetCell(1).StringCellValue;
                readClient.Patronymic = rowData.GetCell(2).StringCellValue;

                long.TryParse(rowData.GetCell(3).StringCellValue, out long phones);

                readClient.Phone = phones;
                readClient.SeriesAndNumberPasport = rowData.GetCell(4).StringCellValue;

                clientsList.Append(readClient);
            }
            return clientsList;

        }

        public void ReWriteDataInFileXLSX(string filePathAndName, int listForChanged, string changedTextOrObject)
        {
            //Открываем книгу и получаем лист
            IWorkbook workbook;

            using (FileStream fileStream = new FileStream(filePathAndName, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = new XSSFWorkbook(fileStream);

                ISheet sheet = workbook.GetSheetAt(listForChanged);

                //Обновляем данные ячейки
                IRow row = sheet.GetRow(listForChanged);
                row.GetCell(0).SetCellValue(changedTextOrObject);

                //Сохраняем документ
                workbook.Write(fileStream, false);
            }
        }

        //public bool DeleteClientInFileXLSX(string filePathAndName, Client client)
        //{
        //    var res = false;

        //    var oldDataInFile = ReadFileXLSX(filePathAndName);

        //    foreach (var client in oldDataInFile)
        //    {

        //    }
        //}
    }
}
