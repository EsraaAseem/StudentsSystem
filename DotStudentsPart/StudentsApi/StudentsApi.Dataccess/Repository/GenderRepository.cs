using StudentsApi.Dataccess.Data;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Repository
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        private ApplicatioDbContext _db;
        public GenderRepository(ApplicatioDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Gender gender)
        {
            var obj = _db.gender.FirstOrDefault(u => u.genderId == gender.genderId);
            if(obj!=null)
            {
                obj.description = gender.description;
            }
        }
    }
}
