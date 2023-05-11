using StudentsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentsApi.Dataccess.Repository.IRepository
{
    public interface IGenderRepository : IRepository<Gender>
    {
        public void Update(Gender gender);
    }
}
