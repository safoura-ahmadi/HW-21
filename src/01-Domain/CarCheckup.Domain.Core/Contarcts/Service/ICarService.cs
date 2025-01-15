using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Service;
public interface ICarService
{
    //Create
    int Create(Car car);
    bool IsPlateValid(string plate);
    int GetGenerationYearById(int id);
    public bool IsCarIdValid(int id);
}
