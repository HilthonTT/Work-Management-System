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
    [Authorize]
    [Route("GetMachines")]
    public List<MachineModel> GetMachines()
    {
        return _stockData.GetMachines();
    }

    
    // Added this method for the WPF AdminStockViewModel so only admin can only access this page
    // Gives out an error when ViewLoaded.
    [HttpGet]
    [Authorize]
    [Route("Admin/GetMachines")]
    public List<MachineModel> GetMachinesAdmin()
    {
        return _stockData.GetMachines();
    }

    [HttpPost]
    [Authorize]
    [Route("GetMachineByModelName")]
    public List<MachineModel> GetMachineByModelName(string ModelName)
    {
        return _stockData.GetMachineByName(ModelName);
    }

    [HttpPost]
    [Authorize]
    [Route("GetMachineById")]
    public List<MachineModel> GetMachineById(int Id)
    {
        return _stockData.GetMachineById(Id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertMachine")]
    public void InsertMachine(MachineModel machine)
    {
        _stockData.InsertMachine(machine);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdateMachine")]
    public void UpdateMachine(MachineModel machine)
    {
        _stockData.UpdateMachine(machine);
    }


    public record GettingId
    (
        int Id
    );

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/DeleteMachine")]
    public void DeleteMachine(GettingId model)
    {
        _stockData.DeleteMachine(model.Id);
    }

    // Part Table section

    [HttpGet]
    [Authorize]
    [Route("GetParts")]
    public List<PartModel> GetParts() 
    {
        return _stockData.GetParts();
    }


    // Added this method for the WPF AdminStockViewModel so only admin can only access this page
    // Gives out an error when ViewLoaded.
    [HttpGet]
    [Authorize]
    [Route("Admin/GetParts")]
    public List<PartModel> GetPartsAdmin()
    {
        return _stockData.GetParts();
    }


    public record GettingModelName 
    (
        string ModelName
    );

    [HttpPost]
    [Authorize]
    [Route("GetPartByModelName")]
    public List<PartModel> GetPartByModelName(GettingModelName model)
    {
        return _stockData.GetPartByModelName(model.ModelName);
    }

    [HttpPost]
    [Authorize]
    [Route("GetPartById")]
    public List<PartModel> GetPartById(GettingId model)
    {
        return _stockData.GetPartById(model.Id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/InsertPart")]
    public void InsertPart(PartModel part)
    {
        _stockData.InsertPart(part);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/UpdatePart")]
    public void UpdatePart(PartModel part)
    {
        _stockData.UpdatePart(part);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/DeletePart")]
    public void DeletePart(GettingId model)
    {
        _stockData.DeletePart(model.Id);
    }
}
