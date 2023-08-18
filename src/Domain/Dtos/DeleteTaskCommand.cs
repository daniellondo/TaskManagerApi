namespace Domain.Dtos
{
    public class DeleteTaskCommand : CommandBase<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
