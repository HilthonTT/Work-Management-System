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

    public List<MachineModel> GetMachines()
    {
        var output = _sql.LoadData<MachineModel, dynamic>("dbo.spMachine_GetAll", new { }, "WSMData");

        return output;
    }

    public List<MachineModel> GetMachineByName(string MachineName)
    {
        var output = _sql.LoadData<MachineModel, dynamic>("dbo.spMachine_GetByName", new { MachineName }, "WSMData");

        return output;
    }

    public List<MachineModel> GetMachineById(int Id)
    {
        var output = _sql.LoadData<MachineModel, dynamic>("dbo.spMachine_GetById", new { Id }, "WSMData");

        return output;
    }

    public void InsertMachine(MachineModel machine)
    {
        _sql.SaveData("dbo.spMachine_Insert", machine, "WSMData");
    }

    public void UpdateMachine(MachineModel machine)
    {
        _sql.SaveData("dbo.spMachine_Update", machine, "WSMData");
    }

    public void DeleteMachine(int Id)
    {
        _sql.SaveData("dbo.spMachine_Delete", new { Id }, "WSMData");
    }
}
