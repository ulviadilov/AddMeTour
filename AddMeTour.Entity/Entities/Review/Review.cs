using AddMeTour.Core.Entities;

namespace AddMeTour.Entity.Entities.Review;

public class Review : EntityBase
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; }
    public string? Subject { get; set; }
    public string Message { get; set; }
    public DateTime Created { get; set; }

    public Review()
    {
        Created = DateTime.Now;
      
    }
}
