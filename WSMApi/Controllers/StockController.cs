using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockData _stockData;

    public StockController(IStockData stockData)
	{
        _stockData = stockData;
    }

    // Machine Table section

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("GetMachines")]
    public List<MachineModel> GetMachines()
    {
        return _stockData.GetMachines();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("GetMachineByName")]
    public List<MachineModel> GetMachineByName(string MachineName)
    {
        return _stockData.GetMachineByName(MachineName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("GetMachineById")]
    public List<MachineModel> GetMachineById(int Id)
    {
        return _stockData.GetMachineById(Id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("InsertMachine")]
    public void InsertMachine(MachineModel machine)
    {
        _stockData.InsertMachine(machine);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("UpdateMachine")]
    public void UpdateMachine(MachineModel machine)
    {
        _stockData.UpdateMachine(machine);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("DeleteMachine")]
    public void DeleteMachine(int Id)
    {
        _stockData.DeleteMachine(Id);
    }

    // Part Table section

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("GetParts")]
    public List<PartModel> GetParts() 
    {
        return _stockData.GetParts();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("GetPartByModelName")]
    public List<PartModel> GetPartByModelName(string ModelName)
    {
        return _stockData.GetPartByModelName(ModelName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("GetPartById")]
    public List<PartModel> GetPartById(int Id)
    {
        return _stockData.GetPartById(Id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("InsertPart")]
    public void InsertPart(PartModel part)
    {
        _stockData.InsertPart(part);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("UpdatePart")]
    public void UpdatePart(PartModel part)
    {
        _stockData.UpdatePart(part);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public void DeletePart(int Id)
    {
        _stockData.DeletePart(Id);
    }
}
