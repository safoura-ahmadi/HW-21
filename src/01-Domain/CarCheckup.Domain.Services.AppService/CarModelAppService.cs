using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.AppService;

public class CarModelAppService(ICarModelService carModelService) : ICarModelAppService
{
    private readonly ICarModelService _carModelService = carModelService;

    public async Task< Result> Create(string name,CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return new Result(false, "نام مدل نمیتواند خالی باشد");
        var result = await _carModelService.Create(name, cancellationToken);
        if (result)
            return new Result(true, "مدل جدید با موفقیت اصافه شد");
        return new Result(false, "DataBase Error Raised");
    }

    public async Task< Result> Delete(int id, CancellationToken cancellationToken)
    {
       var result = await _carModelService.Delete(id, cancellationToken);
        if (result)
            return new Result(true, "مدل با موفقیت حذف گردید");
        return new Result(false, "مدلی با این ایدی در دیتابیس وجود ندارد");
    }

    public async Task< List<CarModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _carModelService.GetAll(cancellationToken);
    }

    public async Task< CarModel?> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _carModelService.GetById(id, cancellationToken);
        }
        catch
        {
            return null;
        }
    }

    public async Task< Result> UpdateName(int id, string name,CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return new Result(false, "نام مدل نمیتواند خال باشد");
        var result = await _carModelService.UpdateName(id, name,cancellationToken);
        if (result) return new Result(true, "نام مدل با موفقیت ویرایش شد");
        return new Result(false, "مدلی با این ایدی در دیتابیس وجود ندارد");
    }
}
