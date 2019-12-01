using BooksManagementLib;
using System.ServiceModel;

namespace ManagerService
{
    [ServiceContract]
    public interface IManager
    {
        [OperationContract]
        Book[] FindBooks(string[] specifications, bool simple = true); // просто 
        [OperationContract]
        void GetBook(Book book);
        [OperationContract]
        bool MakeOrder(string[] specifications);
        [OperationContract]
        void UpdateInfo();
    }
}
