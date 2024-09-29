using DTOs.ResponseDTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _myDbContext;

        public UserRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<GetUserByTaskStatus> GetUserByTaskStatus(int status)
        {
            var statusnumber = new SqlParameter("@TaskId", status);
            var task = await _myDbContext.Tasks
                .FromSqlRaw("EXEC  @TaskId", statusnumber)
                .FirstOrDefaultAsync();

            return null;
        }
    }
}
