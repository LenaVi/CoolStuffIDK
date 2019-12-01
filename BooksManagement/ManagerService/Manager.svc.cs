using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BooksManagementLib;

namespace ManagerService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Manager : IManager
    {
        public Manager()
        {
            //

        }
        private Warehouse warehouse;
        private List<Publisher> publishers;
        private ISearchProcessor SimpleProcessor; 
        private ISearchProcessor ComplicatedProcessor;
        public Book[] FindBooks(string[] specifications, bool simple = true)
        {
            Book[] searchingResult = null;

            if(simple)
            {
                searchingResult = SimpleProcessor.FindInWarehouse(warehouse);

            }
            else
                searchingResult = ComplicatedProcessor.FindInWarehouse(warehouse);


            return searchingResult;
        }

        public void GetBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool MakeOrder(string[] specifications)
        {
            throw new NotImplementedException();
        }

        public void UpdateInfo()
        {
            throw new NotImplementedException();
        }
    }
}
