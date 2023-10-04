using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IExcelFileHandler
    {
        Task AddUnpToExcelFile(string fileName, long unp, string name, string status);
        Task AddUnpToExcelFile(string fileName, string name, string status);
        List<Client> GetClients(string filename);
        List<Client> GetFilteredClients(DateOnly date, string filename);
    }
}