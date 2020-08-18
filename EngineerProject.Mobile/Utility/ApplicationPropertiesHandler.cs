using System.Threading.Tasks;
using Xamarin.Forms;

namespace EngineerProject.Mobile.Views.Home
{
    public static class ApplicationPropertiesHandler
    {
        public static async Task AddProperty(string key, object value)
        {
            var app = Application.Current;

            if (app.Properties.ContainsKey(key))
                app.Properties[key] = value;
            else
                app.Properties.Add(key, value);

            await app.SavePropertiesAsync();
        }

        public static async Task RemoveProperty(string key)
        {
            var app = Application.Current;

            if (app.Properties.ContainsKey(key))
            {
                app.Properties.Remove("token");

                await app.SavePropertiesAsync();
            }
        }
    }
}