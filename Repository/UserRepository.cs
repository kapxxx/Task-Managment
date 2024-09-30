using DTOs.RequestDTO;
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

        public async Task<List<UserTaskDTO>> GetAllUserTasks(UserTaskFilterDTO input)
        {
            var parameters = new[]
   {
        new SqlParameter("@TaskTitle", input.FilterText ?? (object)DBNull.Value),
        new SqlParameter("@Username", input.FilterText ?? (object)DBNull.Value),
        new SqlParameter("@Email", input.FilterText ?? (object)DBNull.Value),
        new SqlParameter("@PhoneNumber", input.FilterText ?? (object)DBNull.Value),
        new SqlParameter("@SortColumn", input.SortColumn),
        new SqlParameter("@SortOrder", input.SortOrder),
        new SqlParameter("@TaskStatus", input.TaskStatus ?? (object)DBNull.Value),
        new SqlParameter("@TaskPriority", input.TaskPriority ?? (object)DBNull.Value),
        new SqlParameter("@StartDate", input.StartDate ?? (object)DBNull.Value),
        new SqlParameter("@EndDate", input.EndDate ?? (object)DBNull.Value),
        new SqlParameter("@PageNumber", input.PageNumber),
        new SqlParameter("@PageSize", input.PageSize)
    };
            var result= await _myDbContext.Set<UserTaskDTO>()
        .FromSqlRaw("EXEC GetUsersAndTasks @TaskTitle, @Username, @Email, @PhoneNumber, @SortColumn, @SortOrder, @TaskStatus, @TaskPriority, @StartDate, @EndDate, @PageNumber, @PageSize", parameters)
        .ToListAsync();
            return result;

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
