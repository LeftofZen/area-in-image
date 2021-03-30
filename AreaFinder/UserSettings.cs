using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace AreaFinder
{
	public class UserSettings : INotifyPropertyChanged
	{
		public int RGBThreshold
		{
			get => rgbThreshold;
			set
			{
				if (value != rgbThreshold)
				{
					rgbThreshold = value;
					NotifyPropertyChanged();
				}
			}
		}
		private int rgbThreshold;

		public int HSBThreshold
		{
			get => hsbThreshold;
			set
			{
				if (value != hsbThreshold)
				{
					hsbThreshold = value;
					NotifyPropertyChanged();
				}
			}
		}
		private int hsbThreshold;

		public int PixelRatio
		{
			get => pixelRatio;
			set
			{
				if (value != pixelRatio)
				{
					pixelRatio = value;
					NotifyPropertyChanged();
				}
			}
		}
		private int pixelRatio;

		public UserSettings()
		{
			Properties = new BindingList<Property>();
		}

		public string FilenameOfImage
		{
			get => filenameOfImage;
			set
			{
				if (value != filenameOfImage)
				{
					filenameOfImage = value;
					NotifyPropertyChanged();
				}
			}
		}
		private string filenameOfImage;

		public BindingList<Property> Properties
		{ get; set; }

		public static void Save(UserSettings settings, string filename)
			=> File.WriteAllText(filename, JsonSerializer.Serialize(settings));

		public static UserSettings Load(string filename)
			=> JsonSerializer.Deserialize<UserSettings>(File.ReadAllText(filename));

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
