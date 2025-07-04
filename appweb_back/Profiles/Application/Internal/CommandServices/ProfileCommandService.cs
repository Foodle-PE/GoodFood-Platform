using appweb_back.Profiles.Domain.Model.Aggregates;
using appweb_back.Profiles.Domain.Model.Commands;
using appweb_back.Profiles.Domain.Repositories;
using appweb_back.Profiles.Domain.Services;
using appweb_back.Shared.Domain.Repositories;

namespace appweb_back.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWork unitOfWork):IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the profile: {e.Message}");
            return null;
        }
    }
    public async Task<Profile?> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByUserIdAsync(command.UserId);
        if (profile == null) return null;

        profile.UpdateName(command.FirstName, command.LastName);
        profile.UpdateEmail(command.Email);
        profile  .UpdatePhone(command.Phone);
            

        await profileRepository.SaveAsync(profile);
        return profile;
    }
    
    
}