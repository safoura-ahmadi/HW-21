using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;
using CarCheckup.Infra.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
var optionsBuilder = new DbContextOptionsBuilder<CarCheckupDbContext>();
optionsBuilder.UseSqlServer("Data Source=WIN10\\SQLEXPRESS;Initial Catalog=CarCheckupDb;Integrated Security=True;Trust Server Certificate=True");
CarCheckupDbContext dbContext = new CarCheckupDbContext(optionsBuilder.Options);
CarRepository carRepository = new CarRepository(dbContext);
var c = new Car()
{
    Company = CarCheckup.Domain.Core.Enums.Car.CarCompanyEnum.Saipa,
     GenerationYear = 1399,
      ModelId = 2,
       OwnerMeliCode = "0879987789",
        OwnerMobile="09898989989",
         Plate = "33و268-99",
          
};
var y = carRepository.Create(c);
Console.WriteLine();
