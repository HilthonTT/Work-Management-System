using UI.Library.Models;

namespace UI.Library.API
{
    public interface IStockEndpoint
    {
        Task DeleteMachine(int Id);
        Task DeletePart(int Id);
        Task<List<MachineModel>> GetAllMachines();
        Task<List<PartModel>> GetAllParts();
        Task<List<MachineModel>> GetMachineById(int Id);
        Task<List<MachineModel>> GetMachineByModelName(string ModelName);
        Task<List<PartModel>> GetPartById(int Id);
        Task<List<PartModel>> GetPartByModelName(string ModelName);
        Task InsertMachine(MachineModel machine);
        Task InsertPart(PartModel part);
        Task UpdateMachine(MachineModel machine);
        Task UpdatePart(PartModel part);
    }
}