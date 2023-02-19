using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class StockData : IStockData
{
    private readonly ISqlDataAccess _sql;

    public StockData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    // All the stored procedures for the Machine Table
    public List<MachineModel> GetMachines()
    {
        var output = _sql.LoadData<MachineModel, dynamic>("dbo.spMachine_GetAll", new { }, "WSMData");

        return output;
    }

    public MachineModel GetMachineById(MachineModel machine)
    {
        var output = _sql.LoadData<MachineModel, dynamic>("dbo.spMachine_GetById", new { machine.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertMachine(MachineModel machine)
    {
        _sql.SaveData("dbo.spMachine_Insert", machine, "WSMData");
    }

    public void UpdateMachine(MachineModel machine)
    {
        _sql.SaveData("dbo.spMachine_Update", machine, "WSMData");
    }

    public void ArchiveMachine(MachineModel machine)
    {
        _sql.SaveData("dbo.spMachine_Archive", new { machine.Id, machine.Archived }, "WSMData");
    }

    // All the stored procedures for the Part Table

    public List<PartModel> GetParts()
    {
        var output = _sql.LoadData<PartModel, dynamic>("dbo.spPart_GetAll", new { }, "WSMData");

        return output;
    }

    public PartModel GetPartById(PartModel part)
    {
        var output = _sql.LoadData<PartModel, dynamic>("dbo.spPart_GetById", new { part.Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public void InsertPart(PartModel part)
    {
        _sql.SaveData("dbo.spPart_Insert", part, "WSMData");
    }

    public void UpdatePart(PartModel part)
    {
        _sql.SaveData("dbo.spPart_Update", part, "WSMData");
    }

    public void ArchivePart(PartModel part)
    {
        _sql.SaveData("dbo.spPart_Archive", new { part.Id, part.Archived }, "WSMData");
    }
}
