namespace AddMeTour.Entity.ViewModels.Review;

public class ReviewVM
{
    public Guid Id { get; set; }
    public bool isActive { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; }
    public string? Subject { get; set; }
    public string Message { get; set; }
    public DateTime Created { get; set; }
}
