using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace appweb_back.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProfileController
{
    
}