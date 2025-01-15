using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Repository;
public interface ICarRepository
{
    //Create
    int Create(Car car);
    bool IsPlateValid(string plate);
    int GetGenerationYearById(int id);
    bool IsCarIdValid(int id);

}
