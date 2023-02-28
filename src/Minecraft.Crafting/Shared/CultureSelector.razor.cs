using System;
using System.Globalization;

namespace Minecraft.Crafting.Shared
{
    /// <summary>
    /// Partial class representing the CultureSelector component, responsible for managing the culture and language of the application.
    /// </summary>
    public partial class CultureSelector
    {
        /// <summary>
        /// Array containing the supported cultures by the application.
        /// </summary>
        private CultureInfo[] supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("fr-FR")
        };

        /// <summary>
        /// Gets or sets the culture of the application.
        /// </summary>
        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentUICulture == value)
                {
                    return;
                }

                var culture = value.Name.ToLower(CultureInfo.InvariantCulture);

                // Construct the query string to be added to the URL
                var uri = new Uri(this.NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                var query = $"?culture={Uri.EscapeDataString(culture)}&" + $"redirectUri={Uri.EscapeDataString(uri)}";

                // Redirect the user to the culture controller to set the cookie
                this.NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
            }
        }
    }
}
