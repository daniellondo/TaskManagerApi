namespace Services.CommandHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Domain.Dtos;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class TaskCommandHandlers
    {
        public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, BaseResponse<bool>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public AddTaskCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BaseResponse<bool>> Handle(AddTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var task = _mapper.Map<TaskEntity>(command);
                    await _context.Tasks.AddAsync(task, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<bool>("Added successfully!", true);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<bool>(ex.Message + " " + ex.StackTrace, false, null);
                }
            }
        }

        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, BaseResponse<bool>>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;
            public UpdateTaskCommandHandler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BaseResponse<bool>> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var task = await _context.Tasks.FirstAsync(task => task.Id == command.Id, cancellationToken);
                    _mapper.Map(command, task);
                    _context.Tasks.Update(task);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<bool>("Updated successfully!", true);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<bool>(ex.Message + " " + ex.StackTrace, false, null);
                }
            }
        }

        public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, BaseResponse<bool>>
        {
            private readonly IContext _context;
            public DeleteTaskCommandHandler(IContext context)
            {
                _context = context;
            }
            public async Task<BaseResponse<bool>> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.Tasks.Where(t => t.Id.Equals(command.Id)).ExecuteDeleteAsync(cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new BaseResponse<bool>("Updated successfully!", true);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<bool>(ex.Message + " " + ex.StackTrace, false, null);
                }
            }
        }
    }
}
