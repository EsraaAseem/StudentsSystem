using StudentsApi.Dataccess.Repository;
using StudentsApi.Dataccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Data

{
    public class UnitOfWrok : IUnitOfWrok
    {
       private ApplicatioDbContext _db;
        public UnitOfWrok(ApplicatioDbContext db)
        {
            _db = db;
            Students = new StudentsRepository(db);
            gender = new GenderRepository(db);


        }
        public IStudentsRepository Students { get; private set; }
        public IGenderRepository gender { get; private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
