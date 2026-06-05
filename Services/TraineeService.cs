using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Services
{
    public class TraineeService : ITraineeService
    {
        private static List<Trainee> _trainees = new List<Trainee>
        {
            new Trainee {Id=0,FirstName="Smit",LastName="Warang",Email="smit@gmail.com",TechStack="CSS",Status="Active",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now}
        };

        public List<TraineeResponse> GetAll()
        {
            List<TraineeResponse> traineesDTO = _trainees.Select(t => new TraineeResponse
            {
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                TechStack = t.TechStack,
                Status = t.Status
            }).ToList();
            return traineesDTO;
        }

        public TraineeResponse? GetById(int Id)
        {
            Trainee? trainee = _trainees.FirstOrDefault(t => t.Id==Id);
            if (trainee==null)
            {
                return null;
            }
            TraineeResponse traineeDTO = new TraineeResponse(trainee);
            return traineeDTO;
        }

        public TraineeResponse Create(CreateTraineeRequest createTrainee)
        {
            Trainee trainee = new Trainee
            {
                Id = _trainees.Max(t => t.Id)+1,
                FirstName = createTrainee.FirstName,
                LastName = createTrainee.LastName,
                Email = createTrainee.Email,
                TechStack = createTrainee.TechStack,
                Status = createTrainee.Status,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _trainees.Add(trainee);
            return new TraineeResponse(trainee);
        }

        // public bool Update(int id, TraineeDto dto)
        // {
        //     var Trainee = GetById(id);
        //     if (Trainee == null) return false;

        //     Trainee.Name = dto.Name;
        //     Trainee.Price = dto.Price;
        //     return true;
        // }

        // public bool Delete(int id)
        // {
        //     var Trainee = GetById(id);
        //     if (Trainee == null) return false;

        //     _Trainees.Remove(Trainee);
        //     return true;
        // }
    }
}
