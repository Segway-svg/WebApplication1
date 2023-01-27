namespace ClientServer.Requests;

public class SongsRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsFamous { get; set; }
    public Guid AlbumId { get; set; }
}