namespace GigAppTest.Models;

public class GigItem
{
	public long Id { get; set; }
	public string? Title { get; set; }
	public long ArtistId { get; set; }
	public long VenueId { get; set; }
}