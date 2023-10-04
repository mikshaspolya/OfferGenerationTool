using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public interface IMainViewModel
    {
        Task StartProcess(DateOnly date);
    }
}