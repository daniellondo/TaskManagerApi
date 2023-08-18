namespace Services.MapperConfiguration
{
    using AutoMapper;
    using Domain.Dtos;
    using Domain.Entities;

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>(MemberList.Source);
            CreateMap<AddTaskCommand, TaskEntity>(MemberList.Source);
            CreateMap<UpdateTaskCommand, TaskEntity>(MemberList.Source);
        }
    }
}
