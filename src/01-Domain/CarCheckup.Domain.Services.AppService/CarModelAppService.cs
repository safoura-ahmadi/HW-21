using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.AppService;

public class CarModelAppService(ICarModelService carModelService) : ICarModelAppService
{
    private readonly ICarModelService _carModelService = carModelService;

    public Result Create(string name)
    {
        var result = _carModelService.Create(name);
        if (result)
            return new Result(true, "مدل جدید با موفقیت اصافه شد");
        return new Result(false, "DataBase Error Raised");
    }

    public Result Delete(int id)
    {
       var result = _carModelService.Delete(id);
        if (result)
            return new Result(true, "مدل با موفقیت حذف گردید");
        return new Result(false, "DataBase Error Raised");
    }

    public List<CarModel> GetAll()
    {
        return _carModelService.GetAll();
    }

    public CarModel? GetById(int id)
    {
        try
        {
            return _carModelService.GetById(id);
        }
        catch
        {
            return null;
        }
    }

    public Result UpdateName(int id, string name)
    {
        var result = _carModelService.UpdateName(id, name);
        if (result) return new Result(true, "نام مدل با موفقیت ویرایش شد");
        return new Result(false, "DataBase Error Raised");
    }
}
