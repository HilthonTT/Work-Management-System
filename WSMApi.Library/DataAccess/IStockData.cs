using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IStockData
    {
        void DeleteMachine(int Id);
        void DeletePart(int Id);
        List<MachineModel> GetMachineById(int Id);
        List<MachineModel> GetMachineByName(string MachineName);
        List<MachineModel> GetMachines();
        List<PartModel> GetPartById(int Id);
        List<PartModel> GetPartByModelName(string ModelName);
        List<PartModel> GetParts();
        void InsertMachine(MachineModel machine);
        void InsertPart(PartModel part);
        void UpdateMachine(MachineModel machine);
        void UpdatePart(PartModel part);
    }
}