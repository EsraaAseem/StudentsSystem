using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Repository.IRepository
{
    public interface IUnitOfWrok
    {
      
        IStudentsRepository Students { get; }
        IGenderRepository gender { get; }


        public void save();
    }
}
