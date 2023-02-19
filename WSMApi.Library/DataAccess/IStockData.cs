using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess
{
    public interface IStockData
    {
        void ArchiveMachine(MachineModel machine);
        void ArchivePart(PartModel part);
        MachineModel GetMachineById(MachineModel machine);
        List<MachineModel> GetMachines();
        PartModel GetPartById(PartModel part);
        List<PartModel> GetParts();
        void InsertMachine(MachineModel machine);
        void InsertPart(PartModel part);
        void UpdateMachine(MachineModel machine);
        void UpdatePart(PartModel part);
    }
}