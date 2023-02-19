using Microsoft.AspNetCore.Authorization;
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
    [Route("GetMachineById")]
    public MachineModel GetMachineById(MachineModel machine)
    {
        return _stockData.GetMachineById(machine);
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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/ArchiveMachine")]
    public void DeleteMachine(MachineModel machine)
    {
        _stockData.ArchiveMachine(machine);
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

    [HttpPost]
    [Authorize]
    [Route("GetPartById")]
    public PartModel GetPartById(PartModel part)
    {
        return _stockData.GetPartById(part);
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
    [Route("Admin/ArchivePart")]
    public void DeletePart(PartModel part)
    {
        _stockData.ArchivePart(part);
    }
}
