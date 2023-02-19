﻿using UI.Library.Models;

namespace UI.Library.API
{
    public interface IStockEndpoint
    {
        Task ArchiveMachineAsync(MachineModel machine);
        Task ArchivePartAsync(PartModel part);
        Task<List<MachineModel>> GetAllMachinesAdminAsync();
        Task<List<MachineModel>> GetAllMachinesAsync();
        Task<List<PartModel>> GetAllPartsAdminAsync();
        Task<List<PartModel>> GetAllPartsAsync();
        Task<MachineModel> GetMachineByIdAsync(MachineModel machine);
        Task<PartModel> GetPartByIdAsync(PartModel part);
        Task InsertMachineAsync(MachineModel machine);
        Task InsertPartAsync(PartModel part);
        Task UpdateMachineAsync(MachineModel machine);
        Task UpdatePartAsync(PartModel part);
    }
}