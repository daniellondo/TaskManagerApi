namespace Domain.Dtos
{
    public class GetTaskQuery : QueryBase<BaseResponse<List<TaskDto>>>
    {
        public int? Id { get; set; }
    }
}
