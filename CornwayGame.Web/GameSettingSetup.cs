using CornwayGame.BL.Model;
using Microsoft.Extensions.Options;

namespace CornwayGame.Web
{
    public class GameSettingSetup : IConfigureOptions<GameSettings>
    {
        private const string SectionName = "GameSettings";
        private readonly IConfiguration _configuration;

        public GameSettingSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(GameSettings options)
        {
            _configuration
                .GetSection(SectionName)
                .Bind(options);
        }
    }
}
