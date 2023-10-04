using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;
using Models;
using System.Linq;


namespace DataAccessLayer
{
    public class ExcelFileHandler : IExcelFileHandler
    {
        public List<Client> GetClients(string fileName)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rows = worksheet.Dimension.Rows;

                List<Client> clients = new List<Client>();

                for (int i = 2; i <= rows; i++)
                {
                    clients.Add(new Client
                    {
                        Name = worksheet.Cells[i, 1].Text,
                        UNP = worksheet.Cells[i, 2].GetValue<long>(),
                        Date = DateOnly.FromDateTime(worksheet.Cells[i, 3].GetValue<DateTime>()),
                        Sum = worksheet.Cells[i, 4].GetValue<decimal>(),
                        Status = worksheet.Cells[i, 5].Text,
                    }); ;
                }
                return clients;
            }
        }

        public List<Client> GetFilteredClients(DateOnly date, string fileName)
        {
            List<Client> clients = GetClients(fileName);
            return clients.Where(client => client.Date <= date).ToList();
        }

        public async Task AddUnpToExcelFile(string fileName, long unp, string name, string status)
        {
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rows = worksheet.Dimension.Rows;

                for (int i = 2; i <= rows; i++)
                {
                    if (worksheet.Cells[i, 1].GetValue<string>() == name)
                    {
                        worksheet.Cells[i, 2].Value = unp;
                        worksheet.Cells[i, 5].Value = status;
                        break;
                    }
                }

                await package.SaveAsync();
            }
        }

        public async Task AddUnpToExcelFile(string fileName, string name, string status)
        {
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rows = worksheet.Dimension.Rows;

                for (int i = 2; i <= rows; i++)
                {
                    if (worksheet.Cells[i, 1].GetValue<string>() == name)
                    {
                        worksheet.Cells[i, 2].Clear();
                        worksheet.Cells[i, 5].Value = status;
                        break;
                    }
                }

                await package.SaveAsync();
            }
        }
    }
}
