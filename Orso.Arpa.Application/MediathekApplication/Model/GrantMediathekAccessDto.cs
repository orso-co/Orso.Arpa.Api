using System;

namespace Orso.Arpa.Application.MediathekApplication.Model;

public class GrantMediathekAccessDto
{
    public Guid PersonId { get; set; }
    public string Notes { get; set; }
}
