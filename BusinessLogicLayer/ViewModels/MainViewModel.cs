using DataAccessLayer.Api;
using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.ViewModels.Commands;
using BusinessLogicLayer.Utilities;
using Serilog;

namespace BusinessLogicLayer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IMainViewModel
    {
        private readonly string EXCEL_FILE_NAME = "Для тестового.xlsx";
        private readonly string API_URL = "http://grp.nalog.gov.by/api/grp-public/search/payer";

        private readonly ILogger _logger;

        private IExcelFileHandler _excelFileHandler;
        private ITaxpayerApiClient _taxpayerApiClient;
        private IOfferService _offerService;

        public MainViewModel(IExcelFileHandler excelFileHandler, ITaxpayerApiClient taxpayerApiClient, IOfferService offerService)
        {
            _excelFileHandler = excelFileHandler;
            _taxpayerApiClient = taxpayerApiClient;
            _offerService = offerService;
            _logger = LoggerConfigurationManager.GetLogger();
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private string _textBox;
        public string TextBox
        {
            get { return _textBox; }
            set
            {
                if (_textBox != value)
                {
                    _textBox = value;
                    OnPropertyChanged("TextBox");
                }
            }
        }

        private RelayCommand _sendDateCommand;
        public RelayCommand SendDateCommand
        {
            get
            {
                if (_sendDateCommand == null)
                {
                    _sendDateCommand = new RelayCommand(
                        async (date) =>
                        {
                            DateOnly dateOnly = DateOnly.FromDateTime((DateTime)date);
                            await StartProcess(dateOnly);
                        },
                        (date) => true
                    );
                }
                return _sendDateCommand;
            }
        }

        public async Task StartProcess(DateOnly date)
        {
            try
            {
                _logger.Information("process started");

                List<Client> clients = _excelFileHandler.GetFilteredClients(date, EXCEL_FILE_NAME);
                _taxpayerApiClient.ApiUrl = API_URL;

                if (clients.Count != 0)
                {
                    AddMessageToTextBox("Клиенты нашлись, отправляем запросы");
                    var taxpayerResponses = await CallApi(clients);

                    AddMessageToTextBox("Обрабатываем ответы");
                    foreach (KeyValuePair<Client, List<TaxpayerResponse>> response in taxpayerResponses)
                    {
                        if (response.Value.Count() != 0)
                        {
                            _logger.Information("response processing");

                            if (response.Value.Where(taxpayer => taxpayer.vkods == "Действующий").Count() == 1)
                            {
                                await MakeOffer(response, date);
                            }
                            else if (response.Value.Where(taxpayer => taxpayer.vkods == "Действующий").Count() > 1)
                            {
                                await _excelFileHandler.AddUnpToExcelFile(EXCEL_FILE_NAME, response.Key.Name, "Ручная проверка");
                            }
                            else
                            {
                                await _excelFileHandler.AddUnpToExcelFile(EXCEL_FILE_NAME, response.Key.Name, "Ошибка");
                            }
                        }
                        else
                        {
                            await _excelFileHandler.AddUnpToExcelFile(EXCEL_FILE_NAME, response.Key.Name, "Ошибка");
                        }
                    }

                    _logger.Information("process completed successfully");
                    AddMessageToTextBox("Успешно завершили");
                }
                else
                {
                    _logger.Warning("clients were not found");
                    MessageBox.Show("Клиенты не нашлись, выберите другую дату");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"ERROR: {ex.Message}");
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void AddMessageToTextBox(string text)
        {
            TextBox += text + Environment.NewLine;
        }

        private async Task<Dictionary<Client, List<TaxpayerResponse>>> CallApi(List<Client> clients)
        {
            List<Task> tasks = new List<Task>();
            Dictionary<Client, List<TaxpayerResponse>> taxpayerResponses = new Dictionary<Client, List<TaxpayerResponse>>();
            Parallel.ForEach(clients, client =>
            {
                var task = Task.Run(async () =>
                {
                    _logger.Information("sending request");
                    taxpayerResponses.Add(client, _taxpayerApiClient.SearchTaxpayer(client.Name).Result);
                    _logger.Information("retrived response");
                });
                tasks.Add(task);
            });

            await Task.WhenAll(tasks);

            return taxpayerResponses;
        }

        private async Task MakeOffer(KeyValuePair<Client, List<TaxpayerResponse>> response, DateOnly date)
        {
            var taxpayer = response.Value.Where(taxpayer => taxpayer.vkods == "Действующий").First();

            Client client = response.Key;
            client.UNP = taxpayer.vunp;
            _logger.Information("start creating offer");
            _offerService.CreateWordOffer(client, date);
            _logger.Information("offer creating successfully completed");
            await _excelFileHandler.AddUnpToExcelFile(EXCEL_FILE_NAME, taxpayer.vunp, response.Key.Name, "Выполнено");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
