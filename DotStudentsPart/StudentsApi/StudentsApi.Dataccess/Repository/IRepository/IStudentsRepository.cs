using Microsoft.AspNetCore.Http;
using StudentsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace StudentsApi.Dataccess.Repository.IRepository
{
    public interface IStudentsRepository:IRepository<Students>
    {
       // Task<Students> GetStudent(Guid id);
        Task<String> UploadImg(IFormFile file);
        public Task<Students> Update(Guid id,Students students);
        public Task<Students> Delete(Guid id);
    }
}
