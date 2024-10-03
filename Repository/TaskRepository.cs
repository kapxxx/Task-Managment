using AutoMapper;
using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MyDbContext _myDbContext;
        private readonly IMapper _mapper;

        public TaskRepository(MyDbContext myDbContext, IMapper mapper)
        {
            _myDbContext = myDbContext;
            _mapper = mapper;
        }

        public async Task<TaskDTO> CreateAsync(TaskCreateDTO input)
        {
            Tasks task = new Tasks
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                AssignedToUserId = input.AssignedToUserId,
                CreatedByUserId = input.CreatedByUserId,
                Description = input.Description,
                Priority = input.Priority,
                Status = DTOs.Enums.TaskStatuses.ToDo,
                DueDate = input.DueDate,
                CreatedAt = DateTime.Now,
                //CreatedBy=input.CreatedBy
                UpdatedAt = DateTime.Now,
                UpdatedBy = input.UpdatedBy


            };
            var result = await _myDbContext.Tasks.AddAsync(task);
            await _myDbContext.SaveChangesAsync();
            return _mapper.Map<TaskDTO>(result.Entity);

        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _myDbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _myDbContext.Tasks.Remove(task);
                await _myDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Task not found.");
            }
        }

        public async Task<List<TaskDTO>> GetAllAsync()
        {
            return _mapper.Map<List<TaskDTO>>(await _myDbContext.Tasks.ToListAsync());
        }

        public async Task<PagedResult<TaskDTO>> GetAllByUserIdAsync(Guid? id, GetTasksByUserIdDto input)
        {
            var totalCountParameter = new SqlParameter("@TotalCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var parameters = new[]
            {
        new SqlParameter("@UserId", id?? (object)DBNull.Value),
        new SqlParameter("@TaskName", input.TaskName ?? (object)DBNull.Value),
        new SqlParameter("@Description", input.Description ?? (object)DBNull.Value),
        new SqlParameter("@TaskStatus", input.TaskStatus ?? (object)DBNull.Value),
        new SqlParameter("@TaskPriority", input.TaskPriority ?? (object)DBNull.Value),
        new SqlParameter("@StartDate", input.StartDate.HasValue ? (object)input.StartDate.Value : DBNull.Value),
        new SqlParameter("@EndDate", input.EndDate.HasValue ? (object)input.EndDate.Value : DBNull.Value),
        new SqlParameter("@SortColumn", input.SortColumn ?? "Name"), // Default sort column
        new SqlParameter("@SortOrder", input.SortOrder ?? "ASC"),   // Default sort order
        new SqlParameter("@PageNumber", input.PageNumber),
        new SqlParameter("@PageSize", input.PageSize),
        totalCountParameter
    };

            var result = await _myDbContext.Set<TaskDTO>()
                .FromSqlRaw("EXEC GetTaskByUserId @UserId, @TaskName, @Description, @SortColumn, @SortOrder, @TaskStatus, @TaskPriority, @StartDate, @EndDate, @PageNumber, @PageSize", parameters)
                .ToListAsync();


            // Get the total count of records (without pagination)
            await _myDbContext.Database.ExecuteSqlRawAsync("EXEC GetTaskCount @UserId, @TaskName, @Description, @TaskStatus, @TaskPriority, @StartDate, @EndDate, @TotalCount OUT", parameters);

            // Get the total count from the output parameter
            int totalRecords = (int)totalCountParameter.Value;

            // Map and return the paginated result
            return new PagedResult<TaskDTO>
            {
                Items = _mapper.Map<List<TaskDTO>>(result),
                TotalRecords = totalRecords,
                PageNumber = input.PageNumber,
                PageSize = input.PageSize
            };
        }


        public async Task<TaskDTO> GetAsync(Guid id)
        {
            var task = await _myDbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }

            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> UpdateAsync(Guid id, TaskUpdateDTO input)
        {
            var task = await _myDbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }

            // Update task properties
            task.Name = input.Name;
            task.AssignedToUserId = input.AssignedToUserId;
            task.Description = input.Description;
            task.Priority = input.Priority;
            task.Status = input.Status; // Make sure TaskStatuses enum is handled properly
            task.DueDate = input.DueDate;
            task.UpdatedAt = DateTime.Now;
            task.UpdatedBy = input.UpdatedBy;

            _myDbContext.Tasks.Update(task);
            await _myDbContext.SaveChangesAsync();
            return _mapper.Map<TaskDTO>(task);
        }
    }
}
