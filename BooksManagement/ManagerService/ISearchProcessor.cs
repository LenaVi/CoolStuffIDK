using BooksManagementLib;

namespace ManagerService
{
    internal interface ISearchProcessor
    {
        Book[] FindInWarehouse(Warehouse warehouse);
    }
}