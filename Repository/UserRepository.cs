using AutoMapper;
using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _myDbContext;
        private readonly IMapper _mapper;

        public UserRepository(MyDbContext myDbContext,IMapper mapper)
        {
            _myDbContext = myDbContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserTaskDTO>> GetAllUserTasks(UserTaskFilterDTO input)
        {
            var totalCountParameter = new SqlParameter("@TotalCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

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
                    new SqlParameter("@PageSize", input.PageSize),
                    totalCountParameter
                };
            var result = await _myDbContext.Set<UserTaskDTO>()
                .FromSqlRaw("EXEC GetUsersAndTasks @TaskTitle, @Username, @Email, @PhoneNumber, @SortColumn, @SortOrder, @TaskStatus, @TaskPriority, @StartDate, @EndDate, @PageNumber, @PageSize", parameters)
                .ToListAsync();
            await _myDbContext.Database.ExecuteSqlRawAsync("EXEC GetUsersAndTasksCount @TaskTitle, @Username, @Email, @PhoneNumber, @SortColumn, @SortOrder, @TaskStatus, @TaskPriority, @StartDate, @EndDate, @TotalCount OUT", parameters);

            // Get the total count from the output parameter
            int totalRecords = (int)totalCountParameter.Value;
            return new PagedResult<UserTaskDTO>
            {
                Items = _mapper.Map<List<UserTaskDTO>>(result),
                TotalRecords = totalRecords,
                PageNumber = input.PageNumber,
                PageSize = input.PageSize
            };

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
