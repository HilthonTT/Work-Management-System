using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IStockData
    {
        void DeleteMachine(int Id);
        List<MachineModel> GetMachineById(int Id);
        List<MachineModel> GetMachineByName(string MachineName);
        List<MachineModel> GetMachines();
        void InsertMachine(MachineModel machine);
        void UpdateMachine(MachineModel machine);
    }
}