namespace AddMeTour.Entity.ViewModels.Review;

public class AddReviewVM
{
    public virtual bool IsActive { get; set; } = true;
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; }
    public string? Subject { get; set; }
    public string Message { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
