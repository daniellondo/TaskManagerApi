namespace Services.QueryHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Domain.Dtos;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class TaskQueryHandlers
    {
        public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, BaseResponse<StatisticsDto>>
        {
            private readonly IContext _context;

            public GetStatisticsQueryHandler(IContext databaseContext)
            {
                _context = databaseContext;
            }

            public async Task<BaseResponse<StatisticsDto>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var response = new StatisticsDto
                    {
                        TotalTasks = await _context.Tasks.CountAsync(cancellationToken),
                        CompletedTasks = await _context.Tasks.CountAsync(task => task.IsCompleted, cancellationToken)
                    };
                    return new BaseResponse<StatisticsDto>("", response);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<StatisticsDto>("Error getting data", null, ex);
                }
            }
        }

        public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, BaseResponse<List<TaskDto>>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;

            public GetTaskQueryHandler(IContext databaseContext, IMapper mapper)
            {
                _context = databaseContext;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<TaskDto>>> Handle(GetTaskQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var tasks = _context.Tasks;
                    if (query.Id is not null)
                        tasks.Where(t => t.Id == query.Id);
                    var response = await tasks.ProjectTo<TaskDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                    return new BaseResponse<List<TaskDto>>("", response);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<List<TaskDto>>("Error getting data", null, ex);
                }

            }
        }
    }
}
