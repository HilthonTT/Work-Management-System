using UI.Library.Models;

namespace UI.Library.API
{
    public interface IStockEndpoint
    {
        Task DeleteMachineAsync(int Id);
        Task DeletePartAsync(int Id);
        Task<List<MachineModel>> GetAllMachinesAsync();
        Task<List<MachineModel>> GetAllMachinesAdminAsync();
        Task<List<PartModel>> GetAllPartsAsync();
        Task<List<PartModel>> GetAllPartsAdminAsync();
        Task<List<MachineModel>> GetMachineByIdAsync(int Id);
        Task<List<MachineModel>> GetMachineByModelNameAsync(string ModelName);
        Task<List<PartModel>> GetPartByIdAsync(int Id);
        Task<List<PartModel>> GetPartByModelNameAsync(string ModelName);
        Task InsertMachineAsync(MachineModel machine);
        Task InsertPartAsync(PartModel part);
        Task UpdateMachineAsync(MachineModel machine);
        Task UpdatePartAsync(PartModel part);
    }
}